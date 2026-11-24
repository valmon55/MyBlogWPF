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
    public class RegisterViewModel : ViewModelBase
    {
        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }
        public RegisterViewModel(IAuthService authService, ModalNavigationStore modalNavigationStore)
        {
            RegisterCommand = new RegisterCommand(this, authService, modalNavigationStore);
            GoToLoginCommand = new GoToLoginCommand();

            BirthdayPicker = new DatePickerViewModel(DateTime.Now.AddYears(-20));
            BirthdayPicker.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(DatePickerViewModel.SelectedDate))
                {
                    OnPropertyChanged(nameof(BirthDate));
                }
            };
        }
        private string firstName;
        public string FirstName 
        { 
            get => firstName;
            set
            {
                firstName = value;
                ErrorString = string.Empty;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string lastName;
        public string LastName 
        { 
            get => lastName;
            set
            {
                lastName = value;
                ErrorString = string.Empty;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string middleName;
        public string MiddleName
        {
            get => middleName;
            set
            {
                middleName = value;
                ErrorString = string.Empty;
                OnPropertyChanged(nameof(MiddleName));
            }
        }
        public DatePickerViewModel BirthdayPicker { get; }
        public DateTime? BirthDate => BirthdayPicker.SelectedDate;
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                ErrorString = string.Empty;
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
                ErrorString = string.Empty;
                OnPropertyChanged(nameof(Login));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                ErrorString = string.Empty;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string passwordConf;
        public string PasswordConf
        {
            get => passwordConf;
            set
            {
                passwordConf = value;
                ErrorString = string.Empty;
                OnPropertyChanged(nameof(PasswordConf));
            }
        }
        public bool CanSubmit => !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName)
                                && !string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password);
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
    }
}
