using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
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

        public Task<bool> DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _apiClient.GetAsync<List<User>>("User/AllUsers");
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
