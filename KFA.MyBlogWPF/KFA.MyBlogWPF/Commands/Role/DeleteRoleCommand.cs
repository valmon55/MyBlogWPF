using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Roles;
using KFA.MyBlogWPF.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Role
{
    public class DeleteRoleCommand : AsyncCommandBase
    {
        private readonly RolesListingItemViewModel _rolesListingItemViewModel;
        private readonly IRoleService _roleService;
        private readonly RolesStore _rolesStore;
        private readonly IApiClient _apiClient;

        public DeleteRoleCommand(RolesListingItemViewModel rolesListingItemViewModel, 
            RolesStore rolesStore, Services.IApiClient apiClient, Services.IRoleService roleService)
        {
            _rolesListingItemViewModel = rolesListingItemViewModel;
            _roleService = roleService;
            _rolesStore = rolesStore;
            _apiClient = apiClient;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _rolesListingItemViewModel.ErrorMessage = null;
            _rolesListingItemViewModel.IsDeleting = true;

            Models.Role role = _rolesListingItemViewModel.Role;
            try
            {
                var success = await _roleService.DeleteRoleAsync(role);
                if( !success )
                {
                    _rolesListingItemViewModel.ErrorMessage = "Не удалось добавить роль на сервере";
                }
            }
            catch (Exception ex)
            {
                _rolesListingItemViewModel.ErrorMessage = $"Исключение: {ex.Message}";
                Debug.WriteLine($"❌ Исключение при удалении: {ex.Message}");
            }
            finally
            {
                _rolesListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
