using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services.DTOs;
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
            var usersRequest = await _apiClient.GetAsync<List<UsersRequest>>("User/AllUsers");
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

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
