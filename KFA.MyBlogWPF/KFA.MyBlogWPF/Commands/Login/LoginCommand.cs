using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class LoginCommand : AsyncCommandBase
    {
        private LoginViewModel _loginViewModel;

        public LoginCommand(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
        }

        //private readonly LoginViewModel _loginViewModel;

        //public LoginCommand(LoginViewModel loginViewModel)
        //{
        //    _loginViewModel = loginViewModel;
        //}

        public override Task ExecuteAsync(object parameter)
        {
            /// Отправляем запрос в API
            /// Получаем ответ, если все ок - делаем вилимой часть "SingedIn"
            _loginViewModel.ErrorString = string.Empty;
            if ((string)parameter != "Admin")
            {
                _loginViewModel.ErrorString = "Login is incorrect";
            }
            else
            {
                SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
            }
            return Task.CompletedTask;
        }
    }
}
