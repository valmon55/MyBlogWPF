using KFA.MyBlogWPF.Models;
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
    public class DeleteRoleCommand : AsyncCommandBase
    {
        private readonly RolesListingItemViewModel _rolesListingItemViewModel;
        private readonly RolesStore _rolesStore;

        public DeleteRoleCommand(RolesListingItemViewModel rolesListingItemViewModel, RolesStore rolesStore)
        {
            _rolesListingItemViewModel = rolesListingItemViewModel;
            _rolesStore = rolesStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _rolesListingItemViewModel.ErrorMessage = null;
            _rolesListingItemViewModel.IsDeleting = true;

            Models.Role role = _rolesListingItemViewModel.Role;
            try
            {
                await _rolesStore.Delete(role.Id);
            }
            catch (Exception)
            {
                _rolesListingItemViewModel.ErrorMessage = $"Failed to delete Role {role.Name}";
            }
            finally
            {
                _rolesListingItemViewModel.IsDeleting = false;
            }

        }
    }
}
