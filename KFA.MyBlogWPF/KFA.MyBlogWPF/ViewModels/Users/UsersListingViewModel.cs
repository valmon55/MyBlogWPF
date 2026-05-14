using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Roles;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersListingViewModel
    {
        private readonly HttpClient _myBlog;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly UsersStore _usersStore;

        private ObservableCollection<UsersListingItemViewModel> _usersListingItemViewModels;
        public IEnumerable<UsersListingItemViewModel> UsersListingItemViewModels => _usersListingItemViewModels;
        public UsersListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore, UsersStore usersStore)
        {
            _myBlog = myBlog;
            _modalNavigationStore = modalNavigationStore;
            _usersStore = usersStore;

            _usersListingItemViewModels = new ObservableCollection<UsersListingItemViewModel>();
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        First_Name = "Admin",
                        Last_Name = "a",
                        Middle_Name = "a"
                    })
                );
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        First_Name = "Fedor",
                        Last_Name = "k",
                        Middle_Name = "a"
                    })
                );
            _usersListingItemViewModels.Add(
                new UsersListingItemViewModel(
                    new User()
                    {
                        First_Name = "user",
                        Last_Name = "u",
                        Middle_Name = "u"
                    })
                );
        }

    }
}