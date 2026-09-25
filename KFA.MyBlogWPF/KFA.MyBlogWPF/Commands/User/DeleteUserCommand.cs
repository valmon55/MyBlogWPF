using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Users;
using Model = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Roles;
using System.Diagnostics;
using KFA.MyBlogWPF.Services;
using System.Data;

namespace KFA.MyBlogWPF.Commands.User
{
    public class DeleteUserCommand : AsyncCommandBase
    {
        private readonly UsersStore _usersStore;
        private readonly UsersListingItemViewModel _usersListingItemViewModel;
        private readonly IApiClient _apiClient;
        private readonly IUserService _userService;
        private bool _isExecuting;

        public DeleteUserCommand(
            UsersListingItemViewModel usersListingItemViewModel, 
            UsersStore usersStore, 
            IApiClient apiClient, 
            IUserService userService)
        {
            _usersStore = usersStore;
            _usersListingItemViewModel = usersListingItemViewModel;
            _apiClient = apiClient;
            _userService = userService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _isExecuting = true;
            _usersListingItemViewModel.ErrorMessage = null;
            _usersListingItemViewModel.IsDeleting = true;

            Model.User user = _usersListingItemViewModel.User;
            try
            {
                var success = await _userService.DeleteUserAsync(user);
                if (!success)
                {
                    _usersListingItemViewModel.ErrorMessage = "Не удалось удалить пользователя на сервере";
                }
            }
            catch(Exception ex)
            {
                _usersListingItemViewModel.ErrorMessage = $"Failed to delete User {user.Last_Name + user.First_Name}";
                Debug.WriteLine($"❌ Исключение при удалении: {ex.Message}");
            }
            finally
            {
                _usersListingItemViewModel.IsDeleting = false;
                _isExecuting = false;
            }
        }
        public override bool CanExecute(object? parameter)
        {
            // 🔥 Защита: нельзя удалять Admin
            if (_usersListingItemViewModel.IsSystemUser)
            {
                Debug.WriteLine($"⛔ Удаление пользователя '{_usersListingItemViewModel.First_Name}' запрещено (системный ползьватель)");
                return false;
            }

            return !_isExecuting && !_usersListingItemViewModel.IsDeleting;
        }
    }
}
