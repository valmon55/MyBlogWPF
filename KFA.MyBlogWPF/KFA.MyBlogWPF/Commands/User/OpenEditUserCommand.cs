using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFA.MyBlogWPF.ViewModels.Roles;
using System.Diagnostics;
using KFA.MyBlogWPF.Services;

namespace KFA.MyBlogWPF.Commands.User
{
    public class OpenEditUserCommand : AsyncCommandBase
    {
        private readonly UsersListingItemViewModel _usersListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UsersStore _usersStore;
        private readonly IApiClient _apiClient;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public OpenEditUserCommand(UsersListingItemViewModel usersListingItemViewModel,
                                ModalNavigationStore modalNavigationStore,
                                UsersStore usersStore,
                                IApiClient apiClient,
                                IUserService userService,
                                IRoleService roleService)
        {
            _usersListingItemViewModel = usersListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _usersStore = usersStore;
            _apiClient = apiClient;
            _userService = userService;
            _roleService = roleService;
        }
        public event EventHandler CanExecuteChanged;
        public override async Task ExecuteAsync(object? parameter)
        {
            Model.User user = _usersListingItemViewModel.User;
            var _allRoles = await _roleService.GetAllRoleAsync();
            EditUserViewModel editUserViewModel = 
                new EditUserViewModel(user, _usersStore, _modalNavigationStore, _apiClient, _userService, _allRoles);
            _modalNavigationStore.CurrentViewModel = editUserViewModel;
        }
        public override bool CanExecute(object? parameter)
        {
            // 🔥 Защита: нельзя редактировать Admin
            if (_usersListingItemViewModel.IsSystemUser)
            {
                Debug.WriteLine($"⛔ Редактирование пользователя '{_usersListingItemViewModel.First_Name}' запрещено (системный пользователь)");
                return false;
            }

            return !_usersListingItemViewModel.IsDeleting;
        }
    }
}
