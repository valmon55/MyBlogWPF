using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Roles;
using KFA.MyBlogWPF.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Role
{
    public class OpenEditRoleCommand : CommandBase
    {
        private readonly RolesListingItemViewModel _rolesListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesStore _rolesStore;
        private readonly IApiClient _apiClient;
        private readonly IRoleService _roleService;

        public OpenEditRoleCommand(
            RolesListingItemViewModel rolesListingItemViewModel, 
            ModalNavigationStore modalNavigationStore, 
            RolesStore rolesStore, 
            Services.IApiClient apiClient,
            IRoleService roleService)
        {
            _rolesListingItemViewModel = rolesListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _rolesStore = rolesStore;
            _apiClient = apiClient;
            _roleService = roleService;
        }

        public override void Execute(object? parameter)
        {
            Models.Role role = _rolesListingItemViewModel.Role;

            EditRoleViewModel editRoleViewModel = new EditRoleViewModel(role, _modalNavigationStore, _rolesStore, _apiClient, _roleService);
            _modalNavigationStore.CurrentViewModel = editRoleViewModel;
        }
    }
}
