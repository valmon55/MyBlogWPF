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

namespace KFA.MyBlogWPF.Commands.Role
{
    public class AddRoleCommand : AsyncCommandBase
    {
        private readonly AddRoleViewModel _addRoleViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesStore _rolesStore;
        private readonly IApiClient _apiClient;
        private readonly IRoleService _roleService;

        public AddRoleCommand(
            AddRoleViewModel addRoleViewModel, 
            ModalNavigationStore modalNavigationStore, 
            RolesStore rolesStore, 
            IApiClient apiClient, 
            IRoleService roleService)
        {
            _addRoleViewModel = addRoleViewModel;
            _modalNavigationStore = modalNavigationStore;
            _rolesStore = rolesStore;
            _apiClient = apiClient;
            _roleService = roleService;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            //Random random = new Random();
            //int n = random.Next(1, 100);

            RoleDetailsFormViewModel formViewModel = _addRoleViewModel.RoleDetailsFormViewModel;
            Models.Role role = new Models.Role()
            {
                //Id = n,
                Name = formViewModel.RoleName,
                Description = formViewModel.Description,
            };
            // Send API request to Add Role
            try
            {
                if (string.IsNullOrEmpty(role.Name))
                {
                    _addRoleViewModel.ErrorMessage = "Имя роли не может быть пустым";
                    return;
                }

                _addRoleViewModel.ErrorMessage = null;
                _addRoleViewModel.IsLoading = true;

                var success = await _roleService.AddRoleAsync(role);
                if (!success)
                {
                    _addRoleViewModel.ErrorMessage = "Не удалось добавить роль на сервере";
                }

            }
            catch (ValidationException ex)
            {
                Debug.WriteLine($"❌ Ошибка валидации при добавлении роли: {ex.Message}");
                _addRoleViewModel.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Исключение при добавлении роли: {ex.Message}");
                _addRoleViewModel.ErrorMessage = $"❌ Исключение при добавлении роли: {ex.Message}";
            }
            finally
            {
                _addRoleViewModel.IsLoading = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
