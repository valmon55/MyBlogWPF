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
        //private readonly LoginViewModel _loginViewModel;

        //public LoginCommand(LoginViewModel loginViewModel)
        //{
        //    _loginViewModel = loginViewModel;
        //}

        public override Task ExecuteAsync(object parameter)
        {
            /// Отправляем запрос в API
            /// Получаем ответ, если все ок - делаем вилимой часть "SingedIn"
            SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
            return Task.CompletedTask;
        }
    }
}
