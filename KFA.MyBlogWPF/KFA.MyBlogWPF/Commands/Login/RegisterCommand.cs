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
        public override Task ExecuteAsync(object parameter)
        {
            ///Выполняется регистрация
            ///если все Ок - выводим SignedIn
            SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
            return Task.CompletedTask;
        }
    }
}
