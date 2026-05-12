using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Stores
{
    public class RolesStore
    {
        public event Action<Role> RoleAdded;
        public event Action<Role> RoleUpdated;
        public event Action<int> RoleDeleted;
        public async Task Add(Role role)
        {
            RoleAdded?.Invoke(role);
        }
        public async Task Update(Role role)
        {
            RoleUpdated?.Invoke(role);
        }

        public async Task Delete(int id)
        {
            RoleDeleted?.Invoke(id);
        }
    }
}
