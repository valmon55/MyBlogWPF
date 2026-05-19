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
        public LoginViewModel LoginViewModel { get; }
        public MainViewModel(ModalNavigationStore modalNavigationStore,
                            TagsViewModel tagsViewModel,
                            RolesViewModel rolesViewModel,
                            LoginViewModel loginViewModel,
                            UsersViewModel usersViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            TagsViewModel = tagsViewModel;
            RolesViewModel = rolesViewModel;
            LoginViewModel = loginViewModel;
            UsersViewModel = usersViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;

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
}
