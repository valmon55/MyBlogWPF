using KFA.MyBlogWPF.Services.DTOs;
using System;
using System.Collections.Generic;
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

        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
