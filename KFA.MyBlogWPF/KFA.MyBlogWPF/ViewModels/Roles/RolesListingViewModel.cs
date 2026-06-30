using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

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

            LoadRolesAsync();

        }

        private async void LoadRolesAsync()
        {
            if (_myBlog is null)
                return;
            try
            {
                var resp = await _myBlog.GetAsync("https://localhost:7007/Role/AllRoles");
                if (resp.IsSuccessStatusCode)
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    var roles = JsonSerializer.Deserialize<List<Role>>(body);

                    if (roles != null && roles.Any())
                    {
                        Roles.Clear();
                        _rolesListingItemViewModels.Clear();
                        foreach (var role in roles)
                        {
                            Roles.Add(role);
                            _rolesListingItemViewModels.Add(new RolesListingItemViewModel(role, _modalNavigationStore, _rolesStore));
                        }
                    }
                }
                else
                {
                    var errorBody = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка загрузки: {resp.StatusCode} \n {errorBody}");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка сети: {ex.Message}");
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Ошибка обработки данных: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        protected override void Dispose()
        {
            _rolesStore.RoleAdded -= RolesStore_RoleAdded;
            _rolesStore.RoleUpdated -= RolesStore_RoleUpdated;
            _rolesStore.RoleDeleted -= RolesStore_RoleDeleted;

            base.Dispose();
        }
        private async void RolesStore_RoleUpdated(Role role)
        {
            RolesListingItemViewModel? roleViewModel =
                _rolesListingItemViewModels.FirstOrDefault(x => x.Role.Id == role.Id);
            var oldRole = roleViewModel.Role;

            if (roleViewModel != null)
            {
                roleViewModel.Update(role);
            }
            try
            {
                var resp = await _myBlog.PostAsJsonAsync("https://localhost:7007/Role/Update", role);
                //var createdTag = await resp.Content.ReadFromJsonAsync<Tag>();
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Ошибка обновления роли: {roleViewModel.RoleName}" +
                        Environment.NewLine + $"Код ошибки: {resp.StatusCode}");
                    //Откат в UI
                    roleViewModel.Update(oldRole);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }
        private void RolesStore_RoleAdded(Role role)
        {
            AddRole(role);
        }

        private async void AddRole(Role role)
        {
            RolesListingItemViewModel roleViewModel = new RolesListingItemViewModel(role, _modalNavigationStore, _rolesStore);
            _rolesListingItemViewModels.Add(roleViewModel);

            try
            {
                var resp = await _myBlog.PostAsJsonAsync("https://localhost:7007/Role/AddRole",
                                    new Role()
                                    {
                                        Name = roleViewModel.RoleName,
                                        Description = roleViewModel.Description
                                    });
                
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Ошибка добавления роли: {roleViewModel.RoleName}" +
                        Environment.NewLine + $"Код ошибки: {resp.StatusCode}");
                    //Откат в UI
                    _rolesListingItemViewModels.Remove(roleViewModel);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private async void RolesStore_RoleDeleted(int id)
        {
            RolesListingItemViewModel? roleViewModel =
                _rolesListingItemViewModels.FirstOrDefault(x => x.Role.Id == id);
            if (roleViewModel != null)
            {
                _rolesListingItemViewModels.Remove(roleViewModel);
            }
            try
            {
                var resp = await _myBlog.DeleteAsync($"https://localhost:7007/Role/Delete?id={id}");
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Ошибка удаления роли: {roleViewModel.RoleName}" +
                        Environment.NewLine + $"Код ошибки: {resp.StatusCode}");
                    //Откат в UI
                    _rolesListingItemViewModels.Add(roleViewModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
