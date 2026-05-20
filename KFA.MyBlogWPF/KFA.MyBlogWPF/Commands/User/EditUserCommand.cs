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
    public class EditUserCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UsersStore _usersStore;
        private readonly EditUserViewModel _editUserViewModel;

        public EditUserCommand(EditUserViewModel editUserViewModel, ModalNavigationStore modalNavigationStore, UsersStore usersStore)
        {
            _editUserViewModel = editUserViewModel;
            _modalNavigationStore = modalNavigationStore;
            _usersStore = usersStore;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return _editUserViewModel.UserDetailsFormViewModel.CanSubmit;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            // Send API request to Add User

            UserDetailsFormViewModel formViewModel = _editUserViewModel.UserDetailsFormViewModel;

            Models.User user = new Models.User()
            {
                Id = _editUserViewModel.UserId,
                First_Name = formViewModel.FirstName,
                Last_Name = formViewModel.LastName,
                Middle_Name = formViewModel.MiddleName,
                Email = formViewModel.Email,
                BirthDate = formViewModel.BirthDate,
                Login = formViewModel.Login,
                Roles = formViewModel.GetSelectedRoles(),
            };
            // Send API request to Edit User

            try
            {
                await _usersStore.Update(user);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
