using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Xps;

namespace KFA.MyBlogWPF.Services
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;
        private readonly FeatureFlags _featureFlags;

        public ApiClient(
            IHttpClientFactory httpClientFactory,
            ApiSettings apiSettings,
            FeatureFlags featureFlags)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings;
            _featureFlags = featureFlags;
        }
        private HttpClient CreateClient()
        {
            // Если включен Mock-режим, возвращаем заглушку (для тестирования без сервера)
            if (_featureFlags.UseMockData)
            {
                // TODO: Вернуть MockHttpClient
            }
            var client = _httpClientFactory.CreateClient("MyBlogApi");
            Debug.WriteLine($"🌐 BaseAddress: {client.BaseAddress}");
            Debug.WriteLine($"🍪 Куки в клиенте: {client.DefaultRequestHeaders.Contains("Cookie")}");

            return client;
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var client = CreateClient();
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json);
            }
            catch(HttpRequestException ex)
            {
                //ApiException
                throw new Exception("Сетевая ошибка при выполнении запроса");
            }
        }

        public async Task<ApiResponse<TResponse>> PostAsync<TRequest,TResponse>(string endpoint, TRequest data)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(endpoint, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            Debug.WriteLine($"➡️ POST {client.BaseAddress}{endpoint}");
            Debug.WriteLine($"📦 Body: {json}");
            Debug.WriteLine($"⬅️ Status Code: {(int)response.StatusCode} {response.StatusCode}");
            Debug.WriteLine($"📄 Response Body: '{responseBody ?? "<null>"}'");
            Debug.WriteLine($"📄 Response Body Length: {responseBody?.Length ?? 0}");
            Debug.WriteLine($"📄 Response Headers: {string.Join(", ", response.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");
            Debug.WriteLine($"🔍 Content-Type: {response.Content.Headers.ContentType?.MediaType}");

            //return response;

            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse<TResponse>.Failure(await ParseErrorResponse(response, responseBody));
            }

            if (string.IsNullOrEmpty(responseBody))
            {
                if (typeof(TResponse) == typeof(bool))
                {
                    var result = (TResponse)(object)true;
                    return ApiResponse<TResponse>.Success(result);
                }
                // Если ожидаем void или пустой ответ - используем default
                if (typeof(TResponse) == typeof(object) ||
                    typeof(TResponse) == typeof(NoContentResponse) ||
                    typeof(TResponse) == typeof(string) )
                {
                    return ApiResponse<TResponse>.Success(default!);
                }

                // Для остальных типов - пробуем создать пустой объект через конструктор
                try
                {
                    // Пытаемся создать экземпляр TResponse через параметрический конструктор
                    // Если у TResponse есть конструктор без параметров
                    var instance = Activator.CreateInstance<TResponse>();
                    return ApiResponse<TResponse>.Success(instance);
                }
                catch (MissingMethodException)
                {
                    // Если нет конструктора без параметров - ошибка
                    return ApiResponse<TResponse>.Failure(new ApiError
                    {
                        Message = $"Сервер вернул пустой ответ, но тип {typeof(TResponse).Name} ожидает данные",
                        Code = (int)response.StatusCode
                    });
                }

                // Если ожидаем другой тип, но ответ пустой - ошибка
                return ApiResponse<TResponse>.Failure(new ApiError
                {
                    Message = "Сервер вернул пустой ответ, но ожидались данные",
                    Code = (int)response.StatusCode
                });
            }
            // Случай 2: Есть тело ответа - десериализуем
            try
            {
                // Для строки - возвращаем как есть
                if (typeof(TResponse) == typeof(string))
                {
                    return ApiResponse<TResponse>.Success((TResponse)(object)responseBody);
                }

                // Для bool - пытаемся распарсить
                if (typeof(TResponse) == typeof(bool) && bool.TryParse(responseBody, out var boolResult))
                {
                    return ApiResponse<TResponse>.Success((TResponse)(object)boolResult);
                }

                // Обычная десериализация JSON
                var result = JsonSerializer.Deserialize<TResponse>(responseBody);
                return ApiResponse<TResponse>.Success(result!);
            }
            catch (JsonException ex)
            {
                Debug.WriteLine($"❌ Ошибка десериализации: {ex.Message}");
                Debug.WriteLine($"📄 Некорректный JSON: {responseBody}");

                // Пробуем интерпретировать как простой тип (int, bool, string)
                try
                {
                    if (typeof(TResponse) == typeof(int) && int.TryParse(responseBody.Trim(), out var intResult))
                        return ApiResponse<TResponse>.Success((TResponse)(object)intResult);

                    if (typeof(TResponse) == typeof(long) && long.TryParse(responseBody.Trim(), out var longResult))
                        return ApiResponse<TResponse>.Success((TResponse)(object)longResult);
                }
                catch(Exception e) 
                {
                    Debug.WriteLine($"❌ Ошибка интерпретации после ошибки десериализации: {e.Message}");
                }

                return ApiResponse<TResponse>.Failure(new ApiError
                {
                    Message = $"Ошибка обработки ответа сервера: {ex.Message}",
                    Details = responseBody,
                    Code = (int)response.StatusCode
                });
            }
        }
        private async Task HandleErrorResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var error = await ParseErrorResponse(response, content);
            //throw new ApiException(error.Message, (int)response.StatusCode);
        }
        private async Task<ApiError> ParseErrorResponse(HttpResponseMessage response, string content)
        {
            try
            {
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(content);
                if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.Message))
                {
                    return new ApiError
                    {
                        Message = errorResponse.Message,
                        Details = errorResponse.Details,
                        Code = (int)response.StatusCode
                    };
                }
            }
            catch { /* Не удалось распарсить */ }

            return new ApiError
            {
                Message = $"Ошибка сервера: {response.StatusCode} - {response.ReasonPhrase}",
                Code = (int)response.StatusCode
            };
        }
        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            return await client.PutAsync(endpoint, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            try
            {
                var client = CreateClient();
                var response = await client.DeleteAsync(endpoint);
                Debug.WriteLine($"➡️ DELETE {client.BaseAddress}{endpoint}");
                Debug.WriteLine($"⬅️ Status: {response.StatusCode}");

                return response;
            }
            catch(HttpRequestException ex)
            {
                Debug.WriteLine($"Возникла ошибка: {ex.Message}");
                throw new HttpRequestException($"Возникла ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Возникла ошибка: {ex.Message}");
                throw new Exception($"Возникла ошибка: {ex.Message}");
            }

        }

        public Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            throw new NotImplementedException();
        }
    }
}
