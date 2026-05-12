using KFA.MyBlogWPF.Commands;
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
    public class RolesListingItemViewModel :ViewModelBase
    {
        public Role Role { get; private set; }
        public string RoleName => Role.Name;
        private bool isDeleting;
        public bool IsDeleting
        {
            get
            {
                return isDeleting;
            }
            set
            {
                isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return ErrorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public RolesListingItemViewModel(Role role, ModalNavigationStore modalNavigationStore, RolesStore tagsStore)
        {
            Role = role;

            //EditCommand = new OpenEditRoleCommand(this, modalNavigationStore, rolesStore);
            //DeleteCommand = new DeleteRoleCommand(this, rolesStore);
        }

        public void Update(Role role)
        {
            Role = role;

            OnPropertyChanged(nameof(RoleName));
        }

    }
}
