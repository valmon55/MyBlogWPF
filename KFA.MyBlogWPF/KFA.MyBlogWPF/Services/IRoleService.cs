using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public interface IRoleService
    {
        Task<bool> AddRoleAsync(Role role);
        Task<List<Role>> GetAllRoleAsync();
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(Role role);
    }
}
