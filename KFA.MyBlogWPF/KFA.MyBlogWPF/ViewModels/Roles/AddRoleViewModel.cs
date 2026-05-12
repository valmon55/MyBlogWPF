using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Role;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Roles
{
    public class AddRoleViewModel : ViewModelBase
    {
        public RoleDetailsFormViewModel RoleDetailsFormViewModel { get; }
        public AddRoleViewModel(ModalNavigationStore modalNavigationStore, RolesStore rolesStore)
        {
            ICommand submitCommand = new AddRoleCommand(this, modalNavigationStore, rolesStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            RoleDetailsFormViewModel = new RoleDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
