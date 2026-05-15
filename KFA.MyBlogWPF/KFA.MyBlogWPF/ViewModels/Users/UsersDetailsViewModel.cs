using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersDetailsViewModel : ViewModelBase
    {
        private readonly SelectedUserStore _selectedUserStore;
        private User SelectedUser => _selectedUserStore.SelectedUser;
        public bool HasSelectedUser => SelectedUser != null;
        public string FullName => (SelectedUser?.Last_Name + " " +
                                    SelectedUser?.First_Name + " " +
                                    SelectedUser?.Middle_Name).Trim() == string.Empty ? 
                                   "Unknown" :
                                    SelectedUser?.Last_Name + " " +
                                    SelectedUser?.First_Name + " " +
                                    SelectedUser?.Middle_Name;
        public string BirthDate => SelectedUser?.BirthDate.ToShortDateString();
        public string Email => SelectedUser?.Email;
        public string Login => SelectedUser?.Login;
        public IEnumerable<Role> Roles => SelectedUser?.Roles;
        public UsersDetailsViewModel(SelectedUserStore selectedUserStore)
        {
            _selectedUserStore = selectedUserStore;
            _selectedUserStore.SelectedUserChanged += _selectedUserStore_SelectedUserChanged;
        }

        protected override void Dispose()
        {
            _selectedUserStore.SelectedUserChanged -= _selectedUserStore_SelectedUserChanged;
            base.Dispose();
        }

        private void _selectedUserStore_SelectedUserChanged()
        {
            OnPropertyChanged(nameof(HasSelectedUser));
            OnPropertyChanged(nameof(FullName));
            OnPropertyChanged(nameof(BirthDate));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Login));
            OnPropertyChanged(nameof(Roles));
        }
    }
}
