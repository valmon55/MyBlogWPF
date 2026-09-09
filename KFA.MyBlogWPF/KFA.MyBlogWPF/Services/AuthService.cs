using KFA.MyBlogWPF.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Xps;

namespace KFA.MyBlogWPF.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiClient _apiClient;

        public AuthService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<bool>> LoginAsync(LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Login))
                throw new ArgumentException("Login не может быть пустым", nameof(loginRequest.Login));
            if (string.IsNullOrWhiteSpace(loginRequest.Password))
                throw new ArgumentException("Пароль не может быть пустым", nameof(loginRequest.Password));
            try
            {
                var response = await _apiClient.PostAsync<LoginRequest, bool>(
                "User/Login",
                    loginRequest);

                if (response == null)
                    return ApiResponse<bool>.Failure(
                        new ApiError { Message = "Сервер вернул пустой ответ при входе" }
                        );
                if (!response.IsSuccess)
                    return response;

                return ApiResponse<bool>.Success(true);
            }
            catch(HttpRequestException ex)
            {
                return ApiResponse<bool>.Failure(new ApiError
                {
                    Message = "Сетевая ошибка. Проверьте подключение к интернету."
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Failure(new ApiError
                {
                    Message = $"Ошибка при входе: {ex.Message}"
                });
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                Debug.WriteLine("🔓 Выполняется выход из системы...");
                var response = await _apiClient.GetAsync<object>("User/Logout");
                Debug.WriteLine("✅ Запрос на выход отправлен успешно");

                return true;
            }
            catch (HttpRequestException ex)
            {
                // Сетевая ошибка - логируем, но НЕ выбрасываем исключение
                Debug.WriteLine($"⚠️ Сетевая ошибка при выходе: {ex.Message}");
                // Возвращаем true, так как выход на клиенте всё равно нужно выполнить
                return true;
            }
            catch (Exception ex)
            {
                // Любая другая ошибка - логируем, но не блокируем выход
                Debug.WriteLine($"⚠️ Ошибка при выходе: {ex.Message}");
                // Возвращаем true, чтобы не блокировать выход на клиенте
                return true;
            }
        }

        public Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
