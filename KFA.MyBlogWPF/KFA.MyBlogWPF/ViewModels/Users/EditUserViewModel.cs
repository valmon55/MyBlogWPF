using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.User;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class EditUserViewModel : ViewModelBase
    {
        public string UserId { get; }
        public UserDetailsFormViewModel UserDetailsFormViewModel { get; }

        public EditUserViewModel(
            User user, 
            UsersStore usersStore, 
            ModalNavigationStore modalNavigationStore, 
            IApiClient apiClient,
            IUserService userService,
            List<Role> allRoles)
        {
            UserId = user.Id;

            ICommand submitCommand = new EditUserCommand(this, modalNavigationStore, usersStore, apiClient, userService);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            UserDetailsFormViewModel = new UserDetailsFormViewModel(submitCommand, cancelCommand, initialBirthday: user.BirthDate)
            {
                FirstName = user.First_Name,
                LastName = user.Last_Name,
                MiddleName = user.Middle_Name,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Login = user.Login,
            };
            UserDetailsFormViewModel.InitRoles(allRoles, user.Roles);
        }
    }
}
