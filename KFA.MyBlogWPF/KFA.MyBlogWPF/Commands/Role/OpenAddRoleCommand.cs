using KFA.MyBlogWPF.Services;
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
    public class OpenAddRoleCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesStore _rolesStore;
        private readonly IApiClient _apiClient;
        private readonly IRoleService _roleService;

        public OpenAddRoleCommand(ModalNavigationStore
            modalNavigationStore, RolesStore rolesStore, IApiClient apiClient, IRoleService roleService)
        {
            _modalNavigationStore = modalNavigationStore;
            _rolesStore = rolesStore;
            _apiClient = apiClient;
            _roleService = roleService;
        }

        public override void Execute(object? parameter)
        {
            AddRoleViewModel addRoleViewModel = new AddRoleViewModel(_modalNavigationStore, _rolesStore, _apiClient, _roleService);
            _modalNavigationStore.CurrentViewModel = addRoleViewModel;
        }
    }
}
