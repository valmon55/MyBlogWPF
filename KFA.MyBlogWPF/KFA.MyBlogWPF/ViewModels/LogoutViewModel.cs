using KFA.MyBlogWPF.Commands.Login;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
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
        public ICommand LogoutCommand { get; }
        public LogoutViewModel(IAuthService authService, ModalNavigationStore modalNavigationStore)
        {
            LogoutCommand = new LogoutCommand(this, authService, modalNavigationStore);
        }
    }
}
