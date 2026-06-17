using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class LogoutCommand : AsyncCommandBase
    {
        private readonly HttpClient _myBlog;

        public LogoutCommand(HttpClient myBlog)
        {
            _myBlog = myBlog;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_myBlog is null)
                return;
            var resp = await _myBlog.GetAsync("https://localhost:7007/User/Logout");
            SessionStateMessenger.SendSessionStateChanged(SessionState.Login);
            return;
        }
    }
}
