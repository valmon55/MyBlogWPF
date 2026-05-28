using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Articles;
using KFA.MyBlogWPF.ViewModels.Roles;
using KFA.MyBlogWPF.ViewModels.Tags;
using KFA.MyBlogWPF.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
        }
        public bool IsLogin => SessionState == SessionState.Login;
        public bool IsRegister => SessionState == SessionState.Register;
        public bool IsSignedIn => SessionState == SessionState.Signedin;
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
        }

        private void OnSessionStateChanged(SessionState state)
        {
            SessionState = state;
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;
            SessionStateMessenger.SessionStateChanged -= OnSessionStateChanged;

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
}
