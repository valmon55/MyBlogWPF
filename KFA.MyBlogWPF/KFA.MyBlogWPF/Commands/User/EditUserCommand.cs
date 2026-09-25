using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Roles;
using KFA.MyBlogWPF.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.User
{
    public class EditUserCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UsersStore _usersStore;
        private readonly EditUserViewModel _editUserViewModel;
        private readonly IApiClient _apiClient;
        private readonly IUserService _userService;

        public EditUserCommand(
            EditUserViewModel editUserViewModel, 
            ModalNavigationStore modalNavigationStore, 
            UsersStore usersStore, 
            IApiClient apiClient, 
            IUserService userService)
        {
            _editUserViewModel = editUserViewModel;
            _modalNavigationStore = modalNavigationStore;
            _usersStore = usersStore;
            _apiClient = apiClient;
            _userService = userService;
        }
        //public event EventHandler CanExecuteChanged;
        //public bool CanExecute(object parameter)
        //{
        //    return _editUserViewModel.UserDetailsFormViewModel.CanSubmit;
        //}
        public override async Task ExecuteAsync(object parameter)
        {
            // Send API request to Add User
            try
            {

                UserDetailsFormViewModel formViewModel = _editUserViewModel.UserDetailsFormViewModel;

                Models.User user = new Models.User()
                {
                    Id = _editUserViewModel.UserId,
                    First_Name = formViewModel.FirstName,
                    Last_Name = formViewModel.LastName,
                    Middle_Name = formViewModel.MiddleName,
                    Email = formViewModel.Email,
                    BirthDate = formViewModel.BirthDate,
                    Login = formViewModel.Login,
                    Roles = formViewModel.GetSelectedRoles(),
                };
                // Send API request to Edit User
                if (string.IsNullOrEmpty(user.First_Name) || string.IsNullOrEmpty(user.Last_Name))
                {
                    _editUserViewModel.ErrorMessage = "ФИО пользователя не может быть пустым";
                    return;
                }

                _editUserViewModel.ErrorMessage = null;
                _editUserViewModel.IsLoading = true;

                var success = await _userService.UpdateUserAsync(user);
                if (!success)
                {
                    _editUserViewModel.ErrorMessage = "Не удалось обновить пользователя на сервере";
                }

                await _usersStore.Update(user);

                _modalNavigationStore.Close();
            }
            catch (ValidationException ex)
            {
                Debug.WriteLine($"❌ Ошибка валидации при обновлении пользователя: {ex.Message}");
                _editUserViewModel.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Исключение при обновлении пользователя: {ex.Message}");
                _editUserViewModel.ErrorMessage = $"❌ Исключение при обновлении пользователя: {ex.Message}";
            }
            finally
            {
                _editUserViewModel.IsLoading = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
