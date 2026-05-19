using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Users;
using Model = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.ViewModels;

namespace KFA.MyBlogWPF.Commands.User
{
    public class DeleteUserCommand : AsyncCommandBase
    {
        private readonly UsersStore _usersStore;
        private readonly UsersListingItemViewModel _usersListingItemViewModel;

        public DeleteUserCommand(UsersListingItemViewModel usersListingItemViewModel, UsersStore usersStore)
        {
            _usersStore = usersStore;
            _usersListingItemViewModel = usersListingItemViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _usersListingItemViewModel.ErrorMessage = null;
            _usersListingItemViewModel.IsDeleting = true;

            Model.User user = _usersListingItemViewModel.User;
            try
            {
                await _usersStore.Delete(user.Id);
            }
            catch(Exception ex)
            {
                _usersListingItemViewModel.ErrorMessage = $"Failed to delete User {user.Last_Name + user.First_Name}";
            }
            finally
            {
                _usersListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
