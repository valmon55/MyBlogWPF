using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Roles;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersListingViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedUserStore _selectedUserStore;
        private readonly UsersStore _usersStore;

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

        public UsersListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore, SelectedUserStore selectedUserStore, UsersStore usersStore)
        {
            _myBlog = myBlog;
            _modalNavigationStore = modalNavigationStore;
            _selectedUserStore = selectedUserStore;
            _usersStore = usersStore;

            _usersListingItemViewModels = new ObservableCollection<UsersListingItemViewModel>();
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        First_Name = "Admin",
                        Last_Name = "a",
                        Middle_Name = "a"
                    },
                    modalNavigationStore,
                    usersStore
                    )
                );
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        First_Name = "Fedor",
                        Last_Name = "k",
                        Middle_Name = "a"
                    },
                    modalNavigationStore,
                    usersStore)
                );
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        First_Name = "user",
                        Last_Name = "u",
                        Middle_Name = "u"
                    },
                    modalNavigationStore,
                    usersStore)
                );
        }

    }
}