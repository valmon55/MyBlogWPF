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

            //_usersStore.UserUpdated += UsersStore_UserUpdated;
            //_usersStore.UserDeleted += UsersStore_UserDeleted;
            _usersStore.UserUpdated += OnUserUpdatedAsync;
            _usersStore.UserUpdated += OnUserDeleted;
            _usersStore.UserUpdated += OnUsersRequested;

            _usersListingItemViewModels = new ObservableCollection<UsersListingItemViewModel>();
            var allRoles = new List<Role>()
            {
                new Role() { Id = "1", Name = "Admin", Description = "Administrator" },
                new Role() { Id = "2", Name = "User", Description = "Ordinal User" },
                new Role() { Id = "3", Name = "Moderator", Description = "Moderator" }
            };

            //_allRoles = allRoles;

            //_usersListingItemViewModels = new ObservableCollection<UsersListingItemViewModel>();
            //_usersListingItemViewModels.Add(
            //    new UsersListingItemViewModel(
            //        new User()
            //        {
            //            Id = 1,
            //            First_Name = "Admin",
            //            Last_Name = "a",
            //            Middle_Name = "a",
            //            Email = "Admin@a.ru",
            //            BirthDate = DateTime.Now,
            //            Login = "Admin",
            //            Roles = new List<Role>()
            //            {
            //                new Role() { Id = "1", Name = "Admin", Description = "Administrator" },
            //                new Role() { Id = "2", Name = "User", Description = "Ordinal User" },
            //            }
            //        },
            //        _allRoles,
            //        modalNavigationStore,
            //        usersStore
            //        )
            //    );
            //_usersListingItemViewModels.Add(
            //    new UsersListingItemViewModel(
            //        new User()
            //        {
            //            Id = 2,
            //            First_Name = "Fedor",
            //            Last_Name = "k",
            //            Middle_Name = "a",
            //            Email = "f@f.ru",
            //            BirthDate = DateTime.Now,
            //            Login = "fedor",
            //            Roles = new List<Role>()
            //            {
            //                new Role() { Id = "3", Name = "Moderator", Description = "Moderator" },
            //                new Role() { Id = "2", Name = "User", Description = "Ordinal User" },
            //            }
            //        },
            //        _allRoles,
            //        modalNavigationStore,
            //        usersStore)
            //    );
            //_usersListingItemViewModels.Add(
            //    new UsersListingItemViewModel(
            //        new User()
            //        {
            //            Id = 3,
            //            First_Name = "user",
            //            Last_Name = "u",
            //            Middle_Name = "u",
            //            Email = "user@u.ru",
            //            BirthDate = DateTime.Now,
            //            Login = "usr",
            //            Roles = new List<Role>()
            //            {
            //                new Role() { Id = "2", Name = "User", Description = "Ordinal User" },
            //            }
            //        },
            //        _allRoles,
            //        modalNavigationStore,
            //        usersStore)
            //    );
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
            //_usersStore.UserUpdated -= UsersStore_UserUpdated;
            //_usersStore.UserDeleted -= UsersStore_UserDeleted;
            _usersStore.UserUpdated -= OnUserUpdatedAsync;
            _usersStore.UserUpdated -= OnUserDeleted;
            _usersStore.UserUpdated -= OnUsersRequested;

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

        private async void OnUsersRequested(User user)
        {
            await ReloadAllUsersAsync();
        }

        private void OnUserDeleted(User user)
        {
            throw new NotImplementedException();
        }

        private void OnUserUpdatedAsync(User user)
        {
            throw new NotImplementedException();
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