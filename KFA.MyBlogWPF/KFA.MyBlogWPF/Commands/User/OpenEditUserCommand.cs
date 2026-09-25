using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFA.MyBlogWPF.ViewModels.Roles;
using System.Diagnostics;

namespace KFA.MyBlogWPF.Commands.User
{
    public class OpenEditUserCommand : CommandBase
    {
        private readonly UsersListingItemViewModel _usersListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UsersStore _usersStore;
        private readonly List<Model.Role> _allRoles;

        public OpenEditUserCommand(UsersListingItemViewModel usersListingItemViewModel, 
                                ModalNavigationStore modalNavigationStore, 
                                UsersStore usersStore,
                                List<Model.Role> allRoles)
        {
            _usersListingItemViewModel = usersListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _usersStore = usersStore;
            _allRoles = allRoles;
        }
        public event EventHandler CanExecuteChanged;
        public override void Execute(object? parameter)
        {
            Model.User user = _usersListingItemViewModel.User;

            EditUserViewModel editUserViewModel = 
                new EditUserViewModel(user, _usersStore, _modalNavigationStore, _allRoles);
            _modalNavigationStore.CurrentViewModel = editUserViewModel;
        }
        public override bool CanExecute(object? parameter)
        {
            // 🔥 Защита: нельзя редактировать Admin
            if (_usersListingItemViewModel.IsSystemUser)
            {
                Debug.WriteLine($"⛔ Редактирование пользователя '{_usersListingItemViewModel.First_Name}' запрещено (системный пользователь)");
                return false;
            }

            return !_usersListingItemViewModel.IsDeleting;
        }
    }
}
