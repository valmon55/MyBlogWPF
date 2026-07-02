using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Models;
using System.Diagnostics;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class LoginCommand : AsyncCommandBase
    {
        private LoginViewModel _loginViewModel;
        private readonly IApiClient _apiClient;
        public LoginCommand(LoginViewModel loginViewModel, IApiClient apiClient)
        {
            _loginViewModel = loginViewModel;
            _apiClient = apiClient;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            /// Отправляем запрос в API
            /// Получаем ответ, если все ок - делаем вилимой часть "SingedIn"

            _loginViewModel.ErrorString = string.Empty;

            try
            {
                const string endpoint = "User/Login";

                var request = new Model.Login() { Email = _loginViewModel.Login, Password = _loginViewModel.Password };

                var responseMessage = await _apiClient.PostAsync(endpoint, request);
                if (responseMessage.IsSuccessStatusCode)
                {
                    SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
                    Debug.WriteLine($"✅ Logged in as {request.Email}");
                }
                else
                {
                    _loginViewModel.ErrorString = responseMessage.StatusCode.ToString();

                    var errorBody = await responseMessage.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Ошибка входа в систему: {responseMessage.StatusCode}");
                    Debug.WriteLine($"📄 Тело ответа: {errorBody}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Исключение: {ex.Message}");
            }
        }
    }
}
