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
using KFA.MyBlogWPF.Services.DTOs;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class LoginCommand : AsyncCommandBase
    {
        private LoginViewModel _loginViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly IAuthService _authService;
        public LoginCommand(LoginViewModel loginViewModel,
            IAuthService authService,
            ModalNavigationStore modalNavigationStore)
        {
            _loginViewModel = loginViewModel;
            _authService = authService;
            _modalNavigationStore = modalNavigationStore;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _loginViewModel.ErrorMessage = string.Empty;
                _loginViewModel.IsLoading = true;

                var request = new LoginRequest() 
                { 
                    Login = _loginViewModel.Login, 
                    Password = _loginViewModel.Password 
                };

                var response = await _authService.LoginAsync(request);

                if (response.IsSuccess)
                {
                    SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
                    Debug.WriteLine($"✅ Logged in as {request.Login}");
                    _modalNavigationStore.Close();
                    //подгружаем роли для пользователей имеющих соответствующие права
                    //вызываем событие
                }
                else
                {
                    _loginViewModel.ErrorMessage = response.Error?.Message ?? "Ошибка входа";
                }
            }
            catch (Exception ex)
            {
                _loginViewModel.ErrorMessage = $"Непредвиденная ошибка: {ex.Message}";
            }
            finally
            {
                _loginViewModel.IsLoading = false;
            }
        }
    }
}
