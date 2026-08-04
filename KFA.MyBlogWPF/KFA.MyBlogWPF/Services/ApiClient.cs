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
                Debug.WriteLine($"⬅️ Status: {response.StatusCode}");
                Debug.WriteLine($"📄 Response: {responseBody}");

            //return response;

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<TResponse>(responseBody);
                return ApiResponse<TResponse>.Success(result);
            }

            return ApiResponse<TResponse>.Failure(await ParseErrorResponse(response, responseBody));
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
            var client = CreateClient();
            return await client.DeleteAsync(endpoint);
        }

        public Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            throw new NotImplementedException();
        }
    }
}
