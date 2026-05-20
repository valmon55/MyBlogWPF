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

        public UsersListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore, SelectedUserStore selectedUserStore, UsersStore usersStore)
        {
            _myBlog = myBlog;
            _modalNavigationStore = modalNavigationStore;
            _selectedUserStore = selectedUserStore;
            _usersStore = usersStore;

            _usersStore.UserUpdated += UsersStore_UserUpdated;
            _usersStore.UserDeleted += UsersStore_UserDeleted;

            var allRoles = new List<Role>()
            {
                new Role() { Id = 1, Name = "Admin", Description = "Administrator" },
                new Role() { Id = 2, Name = "User", Description = "Ordinal User" },
                new Role() { Id = 3, Name = "Moderator", Description = "Moderator" }
            };

            _allRoles = allRoles;

            _usersListingItemViewModels = new ObservableCollection<UsersListingItemViewModel>();
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        Id = 1,
                        First_Name = "Admin",
                        Last_Name = "a",
                        Middle_Name = "a",
                        Email = "Admin@a.ru",
                        BirthDate = DateTime.Now,
                        Login = "Admin",
                        Roles = new List<Role>()
                        {
                            new Role() { Name = "Admin", Description = "Administrator" },
                            new Role() { Name = "User", Description = "Ordinal User" },
                        }
                    },
                    _allRoles,
                    modalNavigationStore,
                    usersStore
                    )
                );
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        Id = 2,
                        First_Name = "Fedor",
                        Last_Name = "k",
                        Middle_Name = "a",
                        Email = "f@f.ru",
                        BirthDate = DateTime.Now,
                        Login = "fedor",
                        Roles = new List<Role>()
                        {
                            new Role() { Name = "Moderator", Description = "Moderator" },
                            new Role() { Name = "User", Description = "Ordinal User" },
                        }
                    },
                    _allRoles,
                    modalNavigationStore,
                    usersStore)
                );
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        Id = 3,
                        First_Name = "user",
                        Last_Name = "u",
                        Middle_Name = "u",
                        Email = "user@u.ru",
                        BirthDate = DateTime.Now,
                        Login = "usr",
                        Roles = new List<Role>()
                        {
                            new Role() { Name = "User", Description = "Ordinal User" },
                        }
                    },
                    _allRoles,
                    modalNavigationStore,
                    usersStore)
                );
        }
        protected override void Dispose()
        {
            _usersStore.UserUpdated -= UsersStore_UserUpdated;
            _usersStore.UserDeleted -= UsersStore_UserDeleted;
            base.Dispose();
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

        private void UsersStore_UserDeleted(int id)
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