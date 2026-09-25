using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Role;
using KFA.MyBlogWPF.Commands.User;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersListingItemViewModel : ViewModelBase
    {
        private bool _isNew;
        public bool IsNew
        {
            get => _isNew;
            set => SetField(ref _isNew, value);
        }
        //private readonly List<Role> _allRoles;
        private User _user;
        public User User 
        { 
            get => _user; 
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
                OnPropertyChanged(nameof(IsNew));
                OnPropertyChanged(nameof(Last_Name));
                OnPropertyChanged(nameof(First_Name));
                OnPropertyChanged(nameof(Middle_Name));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(BirthDate));
                OnPropertyChanged(nameof(Login));
                OnPropertyChanged(nameof(Roles));
            }
        }
        public string Last_Name => User.Last_Name;
        public string First_Name => User.First_Name;
        public string Middle_Name => User.Middle_Name;
        public string Email => User.Email;
        public DateTime BirthDate => User.BirthDate;
        public string Login => User.Login;
        public IEnumerable<Role> Roles => User.Roles;

        private bool isDeleting;
        public bool IsDeleting
        {
            get => isDeleting;
            set => SetField(ref isDeleting, value);
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetField(ref errorMessage, value);
        }
        public bool IsSystemUser =>
            string.Equals(_user.First_Name, "Admin", StringComparison.OrdinalIgnoreCase);
        public bool CanEdit => !IsSystemUser && !IsDeleting;
        public bool CanDelete => !IsSystemUser && !IsDeleting;
        public string? LockReason => IsSystemUser
            ? "Системный пользователь не может быть изменен или удален"
            : null;
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public UsersListingItemViewModel(
            User user, 
            //List<Role> allRoles, 
            ModalNavigationStore modalNavigationStore, 
            UsersStore usersStore,
            IApiClient apiClient,
            IUserService userService,
            IRoleService roleService,
            bool isNew = false)
        {
            User = user;
            //_allRoles = allRoles;
            //var allRoles = roleService.GetAllRoleAsync().Result;
            //var allRoles = new List<Role>();
            EditCommand = new OpenEditUserCommand(this, modalNavigationStore, usersStore, apiClient, userService, roleService);
            DeleteCommand = new DeleteUserCommand(this, usersStore, apiClient, userService);
        }
        public void Update(User user)
        {
            User = user;

            OnPropertyChanged(nameof(Last_Name));
            OnPropertyChanged(nameof(First_Name));
            OnPropertyChanged(nameof(Middle_Name));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Login));
            OnPropertyChanged(nameof(Roles));
            OnPropertyChanged(nameof(IsSystemUser));
            OnPropertyChanged(nameof(CanEdit));
            OnPropertyChanged(nameof(CanDelete));
            OnPropertyChanged(nameof(LockReason));
        }

    }
}
