using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Login;
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
    /// <summary>
    /// Для представления LoginView
    /// Содержит команды Login, Cancel и 
    /// Regster - переход на другую страницу
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        private string login;
        public string Login
        {
            get { return login; }
            set 
            { 
                login = value;
                OnPropertyChanged(nameof(Login));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }
        public bool CanSubmit => !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password);
        public LoginViewModel(HttpClient myBlog)
        {
            _myBlog = myBlog;
            LoginCommand = new LoginCommand();
            GoToRegisterCommand = new GoToRegisterCommand();
        }
    }
}
