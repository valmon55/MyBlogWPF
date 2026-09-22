using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Articles;
using KFA.MyBlogWPF.ViewModels.Roles;
using KFA.MyBlogWPF.ViewModels.Tags;
using KFA.MyBlogWPF.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KFA.MyBlogWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        public ViewModelBase CurrentModalViewModal => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;
        public ArticlesViewModel ArticlesViewModel { get; }
        public TagsViewModel TagsViewModel { get; }
        public UsersViewModel UsersViewModel { get; }
        public RolesViewModel RolesViewModel { get; }
        private SessionState sessionState;
        public SessionState SessionState 
        { 
            get { return sessionState; } 
            set
            {
                sessionState = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLogin));
                OnPropertyChanged(nameof(IsRegister));
                OnPropertyChanged(nameof(IsSignedIn));
                OnPropertyChanged(nameof(IsRolesTabVisible));
            }
        }
        public bool IsLogin => SessionState == SessionState.Login;
        public bool IsRegister => SessionState == SessionState.Register;
        public bool IsSignedIn => SessionState == SessionState.Signedin;
        // 🔥 Видимость вкладки Roles
        private bool _isRolesTabVisible;
        public bool IsRolesTabVisible
        {
            get => _isRolesTabVisible;
            set => SetField(ref _isRolesTabVisible, value);
        }
        public LoginViewModel LoginViewModel { get; }
        public RegisterViewModel RegisterViewModel { get; }
        public LogoutViewModel LogoutViewModel { get; }
        public MainViewModel(ModalNavigationStore modalNavigationStore,
                            TagsViewModel tagsViewModel,
                            RolesViewModel rolesViewModel,
                            LoginViewModel loginViewModel,
                            LogoutViewModel logoutViewModel,
                            UsersViewModel usersViewModel,
                            RegisterViewModel registerViewModel,
                            ArticlesViewModel articlesViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            TagsViewModel = tagsViewModel;
            RolesViewModel = rolesViewModel;
            LoginViewModel = loginViewModel;
            LogoutViewModel = logoutViewModel;
            UsersViewModel = usersViewModel;
            RegisterViewModel = registerViewModel;
            ArticlesViewModel = articlesViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
            SessionStateMessenger.SessionStateChanged += OnSessionStateChanged;
            // 🔥 Подписываемся на событие загрузки ролей
            RolesViewModel.RolesLoaded += OnRolesLoaded;

            // Изначально вкладка скрыта
            IsRolesTabVisible = false;
        }

        private void OnRolesLoaded(bool success)
        {
            // 🔥 Если роли успешно загружены — показываем вкладку
            // Если ошибка — скрываем
            Application.Current?.Dispatcher.Invoke(() =>
            {
                IsRolesTabVisible = success && IsSignedIn;
                Debug.WriteLine($"👁️ IsRolesTabVisible = {IsRolesTabVisible} (success={success}, signedIn={IsSignedIn})");
            });
        }

        private void OnSessionStateChanged(SessionState state)
        {
            SessionState = state;
            // 🔥 При смене состояния — если пользователь вышел, скрываем вкладку Roles
            if (state != SessionState.Signedin)
            {
                IsRolesTabVisible = false;
            }
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;
            SessionStateMessenger.SessionStateChanged -= OnSessionStateChanged;
            RolesViewModel.RolesLoaded -= OnRolesLoaded;
            base.Dispose();
        }
        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModal));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }

    public enum SessionState
    {
        Login,
        Register,
        Signedin
    }
    public enum AuthState
    {
        Unknown,
        Granted,
        Denied
    }
}
