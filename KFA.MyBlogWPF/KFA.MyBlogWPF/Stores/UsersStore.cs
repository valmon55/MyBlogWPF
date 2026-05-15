using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Stores
{
    public class UsersStore
    {
        public event Action<User> UserAdded;
        public event Action<User> UserUpdated;
        public event Action<int> UserDeleted;
        public async Task Add(User user)
        {
            UserAdded?.Invoke(user);
        }
        public async Task Update(User user)
        {
            UserUpdated?.Invoke(user);
        }

        public async Task Delete(int id)
        {
            UserDeleted?.Invoke(id);
        }
    }
}
