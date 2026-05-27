using KFA.MyBlogWPF.Commands.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    public class LogoutViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        public ICommand LogoutCommand { get; }
        public LogoutViewModel(HttpClient myBlog)
        {
            _myBlog = myBlog;
            LogoutCommand = new LogoutCommand();
        }
    }
}
