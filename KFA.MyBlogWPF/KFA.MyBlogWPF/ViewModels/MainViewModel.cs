using KFA.MyBlogWPF.Stores;
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
        public TagsViewModel TagsViewModel { get; }
        public UsersViewModel UsersViewModel { get; }
        public RolesViewModel RolesViewModel { get; }
        public SessionState SessionState { get; set; }
        public bool IsLogin => SessionState == SessionState.Login ? true : false;
        public bool IsRegister => SessionState == SessionState.Register ? true : false;
        public bool IsSignedIn => SessionState == SessionState.Signedin ? true : false;
        public LoginViewModel LoginViewModel { get; }
        public RegisterViewModel RegisterViewModel { get; }
        public LogoutViewModel LogoutViewModel { get; }
        public MainViewModel(ModalNavigationStore modalNavigationStore,
                            TagsViewModel tagsViewModel,
                            RolesViewModel rolesViewModel,
                            LoginViewModel loginViewModel,
                            UsersViewModel usersViewModel,
                            RegisterViewModel registerViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            TagsViewModel = tagsViewModel;
            RolesViewModel = rolesViewModel;
            LoginViewModel = loginViewModel;
            UsersViewModel = usersViewModel;
            RegisterViewModel = registerViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;

            //IsLogin = true;

            //_modalNavigationStore.CurrentViewModel = new EditTagViewModel();
        }
        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

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
