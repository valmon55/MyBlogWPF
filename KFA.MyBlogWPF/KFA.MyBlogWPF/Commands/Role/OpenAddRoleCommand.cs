using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Role
{
    public class OpenAddRoleCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesStore _rolesStore;

        public OpenAddRoleCommand(ModalNavigationStore modalNavigationStore, RolesStore rolesStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _rolesStore = rolesStore;
        }

        public override void Execute(object? parameter)
        {
            AddRoleViewModel addRoleViewModel = new AddRoleViewModel(_modalNavigationStore, _rolesStore);
            _modalNavigationStore.CurrentViewModel = addRoleViewModel;
        }
    }
}
