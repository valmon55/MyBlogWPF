using KFA.MyBlogWPF.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var client = CreateClient();
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            var client = CreateClient();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, content);

            Debug.WriteLine($"➡️ POST {client.BaseAddress}{endpoint}");
            Debug.WriteLine($"📦 Body: {json}");
            Debug.WriteLine($"⬅️ Status: {response.StatusCode}");

            return response;
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
    }
}
