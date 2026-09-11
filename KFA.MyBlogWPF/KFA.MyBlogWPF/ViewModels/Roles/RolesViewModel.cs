using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Role;
using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Roles
{
    public class RolesViewModel : ViewModelBase
    {
        public RolesListingViewModel RolesListingViewModel { get; }
        public ICommand AddRolesCommand { get; }
        public RolesViewModel(
            IApiClient apiClient,
            ApiSettings apiSettings,
            AppSettings appSettings,
            FeatureFlags featureFlags,
            ModalNavigationStore modalNavigationStore,
            RolesStore rolesStore,
            IRoleService roleService)
        {
            RolesListingViewModel = new RolesListingViewModel(
                apiClient, 
                apiSettings,
                appSettings,
                featureFlags,
                modalNavigationStore, 
                rolesStore,
                roleService);

            AddRolesCommand = new OpenAddRoleCommand(modalNavigationStore, rolesStore, apiClient, roleService);
        }

    }
}
