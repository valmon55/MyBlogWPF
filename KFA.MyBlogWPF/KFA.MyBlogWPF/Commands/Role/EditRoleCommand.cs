using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands
{
    public class EditRoleCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesStore _rolesStore;
        private readonly EditRoleViewModel _editRoleViewModel;
        private readonly IApiClient _apiClient;
        private readonly IRoleService _roleService;

        public EditRoleCommand(EditRoleViewModel editRoleViewModel,
            ModalNavigationStore modalNavigationStore,
            RolesStore rolesStore,
            IApiClient apiClient,
            IRoleService roleService)
        {
            _editRoleViewModel = editRoleViewModel;
            _modalNavigationStore = modalNavigationStore;
            _rolesStore = rolesStore;
            _apiClient = apiClient;
            _roleService = roleService;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            // Send API request to Add Role

            RoleDetailsFormViewModel formViewModel = _editRoleViewModel.RoleDetailsFormViewModel;
            Models.Role role = new Models.Role() 
            {
                Id = _editRoleViewModel.RoleId, 
                Name = formViewModel.RoleName,
                Description = formViewModel.Description,
            };
            // Send API request to Edit Role

            try
            {
                await _rolesStore.Update(role);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
