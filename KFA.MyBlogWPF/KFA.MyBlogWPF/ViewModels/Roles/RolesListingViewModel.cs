using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels.Roles
{
    public class RolesListingViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesStore _rolesStore;
        private ObservableCollection<Role> roles;
        public ObservableCollection<Role> Roles
        {
            get => roles;
            set
            {
                roles = value;
                OnPropertyChanged();
            }
        }
        private readonly ObservableCollection<RolesListingItemViewModel> _rolesListingItemViewModels;
        public IEnumerable<RolesListingItemViewModel> RolesListingItemViewModels => _rolesListingItemViewModels;
        public RolesListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore, RolesStore rolesStore)
        {
            _myBlog = myBlog;
            _modalNavigationStore = modalNavigationStore;
            _rolesStore = rolesStore;
            _rolesListingItemViewModels = new ObservableCollection<RolesListingItemViewModel>();
            Roles = new ObservableCollection<Role>();

            _rolesStore.RoleAdded += RolesStore_RoleAdded;
            _rolesStore.RoleUpdated += RolesStore_RoleUpdated;
            _rolesStore.RoleDeleted += RolesStore_RoleDeleted;

        }

        protected override void Dispose()
        {
            _rolesStore.RoleAdded -= RolesStore_RoleAdded;
            _rolesStore.RoleUpdated -= RolesStore_RoleUpdated;
            _rolesStore.RoleDeleted -= RolesStore_RoleDeleted;

            base.Dispose();
        }
        private void RolesStore_RoleUpdated(Role role)
        {
            RolesListingItemViewModel? roleViewModel =
                _rolesListingItemViewModels.FirstOrDefault(x => x.Role.Id == role.Id);

            if (roleViewModel != null)
            {
                roleViewModel.Update(role);
            }
        }
        private void RolesStore_RoleAdded(Role role)
        {
            AddRole(role);
        }

        private void AddRole(Role role)
        {
            RolesListingItemViewModel itemViewModel = new RolesListingItemViewModel(role, _modalNavigationStore, _rolesStore);
            _rolesListingItemViewModels.Add(itemViewModel);
        }

        private void RolesStore_RoleDeleted(int id)
        {
            RolesListingItemViewModel? roleViewModel =
                _rolesListingItemViewModels.FirstOrDefault(x => x.Role.Id == id);
            if (roleViewModel != null)
            {
                _rolesListingItemViewModels.Remove(roleViewModel);
            }
        }

    }
}
