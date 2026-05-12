using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Role
{
    public class AddRoleCommand : AsyncCommandBase
    {
        private readonly AddRoleViewModel _addRoleViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesStore _rolesStore;

        public AddRoleCommand(AddRoleViewModel addRoleViewModel, ModalNavigationStore modalNavigationStore, RolesStore rolesStore)
        {
            _addRoleViewModel = addRoleViewModel;
            _modalNavigationStore = modalNavigationStore;
            _rolesStore = rolesStore;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Random random = new Random();
            int n = random.Next(1, 100);

            RoleDetailsFormViewModel formViewModel = _addRoleViewModel.RoleDetailsFormViewModel;
            Models.Role role = new Models.Role()
            {
                Id = n,
                Name = formViewModel.RoleName
            };
            // Send API request to Edit Role

            try
            {
                await _rolesStore.Add(role);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
