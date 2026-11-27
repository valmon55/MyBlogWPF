using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Roles;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersListingViewModel : ViewModelBase
    {
        private readonly IApiClient _apiClient;
        private readonly ApiSettings _apiSettings;
        private readonly AppSettings _appSettings;
        private readonly FeatureFlags _featureFlags;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedUserStore _selectedUserStore;
        private readonly UsersStore _usersStore;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public string ApplicationName { get; }

        private List<Role> _allRoles;

        private ObservableCollection<UsersListingItemViewModel> _usersListingItemViewModels;
        public IEnumerable<UsersListingItemViewModel> UsersListingItemViewModels => _usersListingItemViewModels;
        
        private UsersListingItemViewModel _selectedUserListingItemViewModel;
        public UsersListingItemViewModel SelectedUserListingItemViewModel
        {
            get 
            { 
                return _selectedUserListingItemViewModel; 
            }
            set 
            { 
                _selectedUserListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedUserListingItemViewModel));

                _selectedUserStore.SelectedUser = _selectedUserListingItemViewModel?.User;
            }
        }

        public UsersListingViewModel(
            IApiClient apiClient,
            ApiSettings apiSettings,
            AppSettings appSettings,
            FeatureFlags featureFlags,
            ModalNavigationStore modalNavigationStore,
            SelectedUserStore selectedUserStore,
            UsersStore usersStore,
            IUserService userService,
            IRoleService roleService)
        {
            _apiClient = apiClient;
            _apiSettings = apiSettings;
            _appSettings = appSettings;
            _featureFlags = featureFlags;
            _modalNavigationStore = modalNavigationStore;
            _selectedUserStore = selectedUserStore;
            _usersStore = usersStore;
            _userService = userService;
            _roleService = roleService;

            _usersStore.UserUpdated += OnUserUpdatedAsync;
            _usersStore.UserDeleted += OnUserDeletedAsync;

            _usersListingItemViewModels = new ObservableCollection<UsersListingItemViewModel>();
        }
        /// <summary>
        /// 🔥 Публичный метод для загрузки пользователей
        /// </summary>
        public void LoadUsers(List<User>? users)
        {
            _usersListingItemViewModels.Clear();

            if (users == null) return;

            foreach (var user in users)
            {
                _usersListingItemViewModels.Add(
                    new UsersListingItemViewModel(
                        user,
                        _modalNavigationStore,
                        _usersStore,
                        _apiClient,
                        _userService,
                        _roleService)
                );
            }

            Debug.WriteLine($"📋 Загружено {_usersListingItemViewModels.Count} пользователей в UI");
        }

        protected override void Dispose()
        {
            _usersStore.UserUpdated -= OnUserUpdatedAsync;
            _usersStore.UserDeleted -= OnUserDeletedAsync;

            base.Dispose();
        }
        private async Task ReloadAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                _usersListingItemViewModels.Clear();

                if (users != null)
                {
                    foreach (var user in users)
                    {
                        _usersListingItemViewModels.Add(
                            new UsersListingItemViewModel(user, _modalNavigationStore, _usersStore, _apiClient, _userService, _roleService)
                        );
                    }
                }
                Debug.WriteLine($"🔄 Загружено {users?.Count ?? 0} пользователей");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Ошибка перезагрузки пользователей: {ex.Message}");
                // Можно показать ошибку пользователю
                ErrorMessage = "Не удалось обновить список пользователей";
            }
        }
        private async Task OnUserDeletedAsync(User user)
        {
            UsersListingItemViewModel? userViewModel = 
                _usersListingItemViewModels.FirstOrDefault(x => x.User.Id == user.Id);
            if (userViewModel != null)
            {
                _usersListingItemViewModels.Remove(userViewModel);
            }
        }

        private async Task OnUserUpdatedAsync(User user)
        {
            foreach(var itemViewModel in UsersListingItemViewModels)
            {
                if(itemViewModel.User.Id == user.Id)
                {
                    itemViewModel.User = user;
                    itemViewModel.User.Roles = user.Roles;
                    break;
                }
            }
            await ReloadAllUsersAsync();
        }

        private void UsersStore_UserUpdated(User user)
        {
            UsersListingItemViewModel userViewModel = 
                _usersListingItemViewModels.FirstOrDefault(x => x.User.Id == user.Id);
            if (userViewModel != null)
            {
                userViewModel.Update(user);
            }
        }

        private void UsersStore_UserDeleted(string id)
        {
            UsersListingItemViewModel userViewModel =
                _usersListingItemViewModels.FirstOrDefault(x => x.User.Id == id);
            if (userViewModel != null)
            {
                _usersListingItemViewModels.Remove(userViewModel);
            }
        }
    }
}