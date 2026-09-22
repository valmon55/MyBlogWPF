using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Role;
using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Roles
{
    public class RolesViewModel : ViewModelBase
    {
        private readonly IRoleService _roleService;
        public RolesListingViewModel RolesListingViewModel { get; }
        public ICommand AddRolesCommand { get; }
        // 🔥 Событие: успешно ли загружены роли
        public event Action<bool>? RolesLoaded;

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
        public RolesViewModel(
            IApiClient apiClient,
            ApiSettings apiSettings,
            AppSettings appSettings,
            FeatureFlags featureFlags,
            ModalNavigationStore modalNavigationStore,
            RolesStore rolesStore,
            IRoleService roleService)
        {
            RolesListingViewModel = new RolesListingViewModel(
                apiClient, 
                apiSettings,
                appSettings,
                featureFlags,
                modalNavigationStore, 
                rolesStore,
                roleService);

            _roleService = roleService;

            AddRolesCommand = new OpenAddRoleCommand(modalNavigationStore, rolesStore, apiClient, roleService);
            // 🔥 Подписываемся на событие смены сессии
            SessionStateMessenger.SessionStateChanged += OnSessionStateChanged;
        }

        private async void OnSessionStateChanged(SessionState state)
        {
            Debug.WriteLine($"🔔 RolesViewModel: SessionState = {state}");

            if (state == SessionState.Signedin)
            {
                // Пользователь вошел — пробуем загрузить роли
                await TryLoadRolesAsync();
            }
            else
            {
                // Пользователь вышел или еще не вошел — очищаем 
                RolesListingViewModel.LoadRoles(null);
                ErrorMessage = null;

                // Сообщаем MainViewModel, что роли не загружены
                RolesLoaded?.Invoke(false);
            }
        }

        private async Task TryLoadRolesAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;

                Debug.WriteLine("🔍 Попытка загрузки ролей...");

                var roles = await _roleService.GetAllRoleAsync();

                // ✅ Успех — роли загружены
                Debug.WriteLine($"✅ Загружено {roles?.Count ?? 0} ролей");
                RolesListingViewModel.LoadRoles(roles);

                // 🔥 Сообщаем MainViewModel, что можно показывать вкладку
                RolesLoaded?.Invoke(true);
            }
            catch (Exception ex)
            {
                // ❌ Любая ошибка — скрываем вкладку
                Debug.WriteLine($"❌ Ошибка загрузки ролей: {ex.Message}");
                ErrorMessage = $"Не удалось загрузить роли: {ex.Message}";

                RolesListingViewModel.LoadRoles(null);

                // 🔥 Сообщаем MainViewModel, что вкладку нужно скрыть
                RolesLoaded?.Invoke(false);
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
