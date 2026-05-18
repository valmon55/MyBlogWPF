using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Role;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersListingItemViewModel : ViewModelBase
    {
        public User User { get; }
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
            get
            {
                return isDeleting;
            }
            set
            {
                isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return ErrorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public UsersListingItemViewModel(User user, ModalNavigationStore modalNavigationStore, UsersStore usersStore)
        {
            User = user;

            EditCommand = new OpenEditUserCommand(this, modalNavigationStore, usersStore);
            //DeleteCommand = new DeleteRoleCommand(this, usersStore);
        }
        public void Update(User user)
        {
            OnPropertyChanged(nameof(Last_Name));
            OnPropertyChanged(nameof(First_Name));
            OnPropertyChanged(nameof(Middle_Name));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Login));
            OnPropertyChanged(nameof(Roles));
        }

    }
}
