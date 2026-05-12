using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Role;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Roles
{
    public class RolesViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        public RolesListingViewModel RolesListingViewModel { get; }
        public ICommand AddRolesCommand { get; }
        public RolesViewModel(HttpClient myBlog,
                            ModalNavigationStore modalNavigationStore,
                            RolesStore rolesStore)
        {
            _myBlog = myBlog;
            RolesListingViewModel = new RolesListingViewModel(_myBlog, modalNavigationStore, rolesStore);

            AddRolesCommand = new OpenAddRoleCommand(modalNavigationStore, rolesStore);
        }

    }
}
