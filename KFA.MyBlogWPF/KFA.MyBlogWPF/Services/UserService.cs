using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public class UserService : IUserService
    {
        private readonly IApiClient _apiClient;
        private readonly UsersStore _usersStore;

        public UserService(IApiClient apiClient, UsersStore usersStore)
        {
            _apiClient = apiClient;
            _usersStore = usersStore;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            try
            {
                var endpoint = $"User/Delete?userId={user.Id}";

                var response = await _apiClient.DeleteAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"✅ Пользователь {user.First_Name} c id = {user.Id} удален");
                    await _usersStore.Delete(user);
                    return true;
                }
                Debug.WriteLine($"Ошибка при удалении пользователя {user.First_Name}");
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Исключение при удалении пользователя: {user.First_Name}");
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            Debug.WriteLine($"⏱️ Запрос всех пользователей");
            var usersRequest = await _apiClient.GetAsync<List<UsersRequest>>("User/AllUsers");
            Debug.WriteLine($"⏱️ Получено {usersRequest?.Count ?? 0} пользователей");
            var users = usersRequest.Select(x => new User()
            {
                Id = x.Id,
                First_Name = x.First_Name,
                Last_Name = x.Last_Name,
                Middle_Name = x.Middle_Name, 
                Email = x.Email,
                BirthDate = x.BirthDate,
                Login = x.Login,
                Roles = x.Roles
            }).ToList();            
            return users;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (string.IsNullOrEmpty(user.First_Name) || string.IsNullOrEmpty(user.Last_Name))
                throw new ArgumentException("ФИО пользователя не может быть пустым", nameof(user.First_Name));

            const string endpoint = "User/Update";
            try
            {
                var request = new EditUserRequest() 
                { 
                    Id = user.Id, 
                    First_Name = user.First_Name,
                    Last_Name = user.Last_Name,
                    Middle_Name = user.Middle_Name,
                    Email = user.Email,
                    // формат "2026-09-25"
                    BirthDate = user.BirthDate.Year.ToString() + "-" +
                                user.BirthDate.Month.ToString() + "-" +
                                user.BirthDate.Day.ToString(),
                    Login = user.Login,
                    Roles = user.Roles
                };
                
                Debug.WriteLine($"⏱️ Начало обновления пользователя ID={user.Id}");
                var response = await _apiClient.PostAsync<EditUserRequest, UserResponse>(endpoint, request);
                Debug.WriteLine($"⏱️ Завершение обновления. Success={response.IsSuccess}");
                if (response.IsSuccess)
                {
                    Debug.WriteLine($"✅ Пользователь c Id: {request.Id} и Name: {request.First_Name} успешно обновлен");
                    await _usersStore.Update(user);
                    return true;
                }
                Debug.WriteLine($"Ошибка при добавлении пользователя {user.First_Name}");
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Исключение при добавлении пользователя c Id: {user.Id} и Name: {user.First_Name}");
            }
        }
    }
}
