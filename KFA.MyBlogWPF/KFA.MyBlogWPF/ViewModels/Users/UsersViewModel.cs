using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        public UsersListingViewModel UsersListingViewModel { get; }
        public UsersDetailsViewModel UsersDetailsViewModel { get; }
        
        public event Action<bool>? UsersLoaded;
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetField(ref _isLoading, value);
        }

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetField(ref _errorMessage, value);
        }
        public UsersViewModel(
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
            _userService = userService;

            UsersListingViewModel = new UsersListingViewModel(
                apiClient,
                apiSettings,
                appSettings,
                featureFlags,
                modalNavigationStore,
                selectedUserStore,
                usersStore,
                userService,
                roleService);

            UsersDetailsViewModel = new UsersDetailsViewModel(selectedUserStore);

            SessionStateMessenger.SessionStateChanged += OnSessionStateChanged;
        }
        private async void OnSessionStateChanged(SessionState state)
        {
            Debug.WriteLine($"🔔 UsersViewModel: SessionState = {state}");

            if (state == SessionState.Signedin)
            {
                // Пользователь вошел — пробуем загрузить пользователей и их роли
                await TryLoadUsersAsync();
            }
            else
            {
                // Пользователь вышел или еще не вошел — очищаем 
                UsersListingViewModel.LoadUsers(null);
                ErrorMessage = null;

                // Сообщаем MainViewModel, что пользователи не загружены
                UsersLoaded?.Invoke(false);
            }
        }

        private async Task TryLoadUsersAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;

                Debug.WriteLine("🔍 Попытка загрузки пользователей...");

                var users = await _userService.GetAllUsersAsync();

                // ✅ Успех — пользователи загружены
                Debug.WriteLine($"✅ Загружено {users?.Count ?? 0} пользователей");
                UsersListingViewModel.LoadUsers(users);

                // 🔥 Сообщаем MainViewModel, что можно показывать вкладку
                UsersLoaded?.Invoke(true);
            }
            catch (Exception ex)
            {
                // ❌ Любая ошибка — скрываем вкладку
                Debug.WriteLine($"❌ Ошибка загрузки пользователей: {ex.Message}");
                ErrorMessage = $"Не удалось загрузить пользователей: {ex.Message}";

                UsersListingViewModel.LoadUsers(null);

                // 🔥 Сообщаем MainViewModel, что вкладку нужно скрыть
                UsersLoaded?.Invoke(false);
            }
            finally
            {
                IsLoading = false;
            }
        }
        protected override void Dispose()
        {
            SessionStateMessenger.SessionStateChanged -= OnSessionStateChanged;
            base.Dispose();
        }
    }
}
