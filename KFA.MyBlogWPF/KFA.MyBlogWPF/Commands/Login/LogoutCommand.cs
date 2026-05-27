using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class LogoutCommand : AsyncCommandBase
    {
        public override Task ExecuteAsync(object parameter)
        {
            SessionStateMessenger.SendSessionStateChanged(SessionState.Login);
            return Task.CompletedTask;
        }
    }
}
