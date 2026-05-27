using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class RegisterCommand : AsyncCommandBase
    {
        private RegisterViewModel _registerViewModel;

        public RegisterCommand(RegisterViewModel registerViewModel)
        {
            _registerViewModel = registerViewModel;
        }

        public override Task ExecuteAsync(object parameter)
        {
            ///Выполняется регистрация
            ///если все Ок - выводим SignedIn
            _registerViewModel.ErrorString = string.Empty;
            if ((string)parameter != "Admin")
            {
                _registerViewModel.ErrorString = "Login is incorrect";
            }
            else
            {
                SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
            }
            return Task.CompletedTask;
        }
    }
}
