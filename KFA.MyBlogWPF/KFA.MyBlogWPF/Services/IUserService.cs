using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
    }
}
