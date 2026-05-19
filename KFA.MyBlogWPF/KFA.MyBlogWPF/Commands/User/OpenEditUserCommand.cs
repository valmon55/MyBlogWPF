using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.User
{
    public class OpenEditUserCommand : CommandBase
    {
        private readonly UsersListingItemViewModel _usersListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UsersStore _usersStore;

        public OpenEditUserCommand(UsersListingItemViewModel usersListingItemViewModel, ModalNavigationStore modalNavigationStore, UsersStore usersStore)
        {
            _usersListingItemViewModel = usersListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _usersStore = usersStore;
        }

        public override void Execute(object? parameter)
        {
            Model.User user = _usersListingItemViewModel.User;

            EditUserViewModel editUserViewModel = new EditUserViewModel(user, _usersStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editUserViewModel;
        }

    }
}
