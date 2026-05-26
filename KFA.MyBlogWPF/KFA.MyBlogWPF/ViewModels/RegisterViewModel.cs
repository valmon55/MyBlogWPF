using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public RegisterViewModel(HttpClient myBlog)
        {
            _myBlog = myBlog;

        }
        private string firstName;
        public string FirstName 
        { 
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string lastName;
        public string LastName 
        { 
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string middleName;
        public string MiddleName
        {
            get => middleName;
            set
            {
                middleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string passwordConf;
        public string PasswordConf
        {
            get => passwordConf;
            set
            {
                passwordConf = value;
                OnPropertyChanged(nameof(PasswordConf));
            }
        }
        public bool CanSubmit => !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password);

    }
}
