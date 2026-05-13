using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Role;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Roles
{
    public class EditRoleViewModel : ViewModelBase
    {
        public int RoleId { get; }
        public RoleDetailsFormViewModel RoleDetailsFormViewModel { get; }
        public EditRoleViewModel(Role role, ModalNavigationStore modalNavigationStore, RolesStore rolesStore)
        {
            RoleId = role.Id;

            ICommand submitCommand = new EditRoleCommand(this, modalNavigationStore, rolesStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            RoleDetailsFormViewModel = new RoleDetailsFormViewModel(submitCommand, cancelCommand)
            {
                RoleName = role.Name,
                Description = role.Description,
            };
        }
    }
}
