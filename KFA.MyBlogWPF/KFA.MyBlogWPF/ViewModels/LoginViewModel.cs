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
                ErrorString = string.Empty;
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
                ErrorString = string.Empty;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string errorString;
        public string ErrorString
        {
            get { return errorString; }
            set
            {
                errorString = value;
                OnPropertyChanged(nameof(ErrorString));
                OnPropertyChanged(nameof(HasErrors));
            }
        }
        public bool HasErrors => !string.IsNullOrEmpty(ErrorString);
        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }
        public bool CanSubmit => !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password);
        public LoginViewModel(HttpClient myBlog)
        {
            _myBlog = myBlog;
            LoginCommand = new LoginCommand(this);
            GoToRegisterCommand = new GoToRegisterCommand();
        }
    }
}
