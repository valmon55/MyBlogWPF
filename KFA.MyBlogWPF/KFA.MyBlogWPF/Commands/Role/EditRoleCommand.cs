using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
            try
            {
                RoleDetailsFormViewModel formViewModel = _editRoleViewModel.RoleDetailsFormViewModel;
                Models.Role role = new Models.Role()
                {
                    Id = _editRoleViewModel.RoleId,
                    Name = formViewModel.RoleName,
                    Description = formViewModel.Description,
                };
                if (string.IsNullOrEmpty(role.Name) || string.IsNullOrEmpty(role.Description))
                {
                    _editRoleViewModel.ErrorMessage = "Имя роли или его описания не может быть пустым";
                    return;
                }

                _editRoleViewModel.ErrorMessage = null;
                _editRoleViewModel.IsLoading = true;

                var success = await _roleService.UpdateRoleAsync(role);
                if (!success)
                {
                    _editRoleViewModel.ErrorMessage = "Не удалось добавить роль на сервере";
                }

            }
            catch (ValidationException ex)
            {
                Debug.WriteLine($"❌ Ошибка валидации при добавлении роли: {ex.Message}");
                _editRoleViewModel.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Исключение при добавлении роли: {ex.Message}");
                _editRoleViewModel.ErrorMessage = $"❌ Исключение при добавлении роли: {ex.Message}";
            }
            finally
            {
                _editRoleViewModel.IsLoading = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
