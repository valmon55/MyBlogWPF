using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        public UsersListingViewModel UsersListingViewModel { get; }
        public UsersDetailsViewModel UsersDetailsViewModel { get; }
        public UsersViewModel(HttpClient myBlog,
                            ModalNavigationStore modalNavigationStore,
                            UsersStore usersStore)
        {
            _myBlog = myBlog;
            UsersListingViewModel = new UsersListingViewModel(_myBlog, modalNavigationStore, usersStore);
            UsersDetailsViewModel = new UsersDetailsViewModel();
        }

    }
}
