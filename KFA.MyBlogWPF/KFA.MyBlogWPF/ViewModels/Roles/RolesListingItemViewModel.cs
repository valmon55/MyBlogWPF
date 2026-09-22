using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Role;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Roles
{
    public class RolesListingItemViewModel : ViewModelBase
    {
        private bool _isNew; 
        public bool IsNew
        {
            get => _isNew;
            set => SetField(ref _isNew, value);
        }
        private Role _role;
        public Role Role
        { 
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
                OnPropertyChanged(nameof(RoleName));
                OnPropertyChanged(nameof(Description));
            }
        }
        public string RoleName => Role.Name;
        public string Description => Role.Description;

        private bool isDeleting;
        public bool IsDeleting
        {
            get => isDeleting;
            set => SetField(ref isDeleting, value);
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetField(ref errorMessage, value);
        }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public RolesListingItemViewModel(
            Role role, 
            ModalNavigationStore modalNavigationStore, 
            RolesStore rolesStore,
            IApiClient apiClient,
            IRoleService roleService,
            bool isNew = false)
        {
            _role = role;

            EditCommand = new OpenEditRoleCommand(this, modalNavigationStore, rolesStore, apiClient, roleService);
            DeleteCommand = new DeleteRoleCommand(this, rolesStore, apiClient, roleService);
        }

        public void Update(Role role)
        {
            _role = role;
            _isNew = false;

            OnPropertyChanged(nameof(Role));
            OnPropertyChanged(nameof(RoleName));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(IsNew));
        }
        protected override void Dispose()
        {
            base.Dispose();
        }
    }
}
