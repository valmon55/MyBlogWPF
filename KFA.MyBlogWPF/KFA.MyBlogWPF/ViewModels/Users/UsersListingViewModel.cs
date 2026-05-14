using KFA.MyBlogWPF.Stores;
using System.Net.Http;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersListingViewModel
    {
        public UsersListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore, UsersStore usersStore)
        {
            MyBlog = myBlog;
            ModalNavigationStore = modalNavigationStore;
            UsersStore = usersStore;
        }

        public HttpClient MyBlog { get; }
        public ModalNavigationStore ModalNavigationStore { get; }
        public UsersStore UsersStore { get; }
    }
}