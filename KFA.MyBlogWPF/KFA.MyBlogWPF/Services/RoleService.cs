using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public class RoleService : IRoleService
    {
        private readonly IApiClient _apiClient;
        private readonly RolesStore _rolesStore;

        public RoleService(IApiClient apiClient, RolesStore rolesStore)
        {
            _apiClient = apiClient;
            _rolesStore = rolesStore;
        }

        public async Task<bool> AddRoleAsync(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Name) || string.IsNullOrWhiteSpace(role.Description))
                Debug.WriteLine("❌ Имя роли и его описание не должно быть пустым");

            const string endpoint = "Role/AddRole";
            try
            {
                var request = new AddRoleRequest() { Name = role.Name.Trim(), 
                    Description = role.Description == null ? "" : role.Description.Trim() };

                var response = await _apiClient.PostAsync<AddRoleRequest, RoleResponse>(endpoint, request);
                if (response.IsSuccess)
                {
                    Debug.WriteLine($"✅ Роль {request.Name} успешно добавлена");
                    
                    await _rolesStore.Add(role);
                    return true;
                }
                Debug.WriteLine($"Ошибка при добавлении роли {role.Name}");
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Исключение при добавлении тега: {role.Name}");
            }
        }

        public async Task<bool> DeleteRoleAsync(Role role)
        {
            try
            {
                var endpoint = $"Role/Delete?roleId={role.Id}";

                var response = await _apiClient.DeleteAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"✅ Роль {role.Name} c id = {role.Id} удалена");
                    await _rolesStore.Delete(role.Id);
                    return true;
                }
                Debug.WriteLine($"Ошибка при удалении роли {role.Name}");
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Исключение при удалении тега: {role.Name}");
            }
        }

        public async Task<List<Role>> GetAllRoleAsync()
        {
            return await _apiClient.GetAsync<List<Role>>("Role/AllRoles");
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Имя роли не должно быть пустым", nameof(role.Name));

            const string endpoint = "Role/Update";
            try
            {
                var request = new EditRoleRequest() { Id = role.Id, Name = role.Name.Trim(), Description = role.Description.Trim() };

                var response = await _apiClient.PostAsync<EditRoleRequest, RoleResponse>(endpoint, request);
                if (response.IsSuccess)
                {
                    Debug.WriteLine($"✅ Роль c Id: {request.Id} и Name: {request.Name} успешно обновлен");
                    await _rolesStore.Update(role);
                    return true;
                }
                Debug.WriteLine($"Ошибка при добавлении роли {role.Name}");
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Исключение при добавлении роли c Id: {role.Id} и Name: {role.Name}");
            }
        }
    }
}
