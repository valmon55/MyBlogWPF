using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class RegisterCommand : AsyncCommandBase
    {
        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthService _authService;
        private readonly ModalNavigationStore _modalNavigationStore;

        public RegisterCommand(RegisterViewModel registerViewModel, 
                IAuthService authService, 
                ModalNavigationStore modalNavigationStore)
        {
            _registerViewModel = registerViewModel;
            _authService = authService;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                ///Выполняется регистрация
                ///если все Ок - выводим SignedIn 
                _registerViewModel.ErrorString = string.Empty;
                _registerViewModel.IsLoading = true;
                var request = new RegisterRequest()
                {
                    FirstName = _registerViewModel.FirstName,
                    LastName = _registerViewModel.LastName,
                    MiddleName = _registerViewModel.MiddleName,
                    Email = _registerViewModel.Email,
                    Year = _registerViewModel.BirthDate.Value.Year,
                    Month = _registerViewModel.BirthDate.Value.Month,
                    Day = _registerViewModel.BirthDate.Value.Day,
                    Password = _registerViewModel.Password,
                    ConfirmPassword = _registerViewModel.PasswordConf,
                    Login = _registerViewModel.Login
                };

                var response = await _authService.RegisterAsync(request);

                if (response.IsSuccess)
                {
                    SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
                    Debug.WriteLine($"✅ Registered as {request.Login}");
                    _modalNavigationStore.Close();
                }
                else
                {
                    _registerViewModel.ErrorMessage = response.Error?.Message ?? "Ошибка регистрации пользователя";
                }
            }
            catch (Exception ex)
            {
                _registerViewModel.ErrorMessage = $"Непредвиденная ошибка: {ex.Message}";
            }
            finally 
            {
                _registerViewModel.IsLoading = false;
            }
        }
    }
}
