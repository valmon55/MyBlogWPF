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
        public event Action<User> UserUpdated;
        public event Action<string> UserDeleted;
        public async Task Update(User user)
        {
            UserUpdated?.Invoke(user);
        }

        public async Task Delete(string id)
        {
            UserDeleted?.Invoke(id);
        }
    }
}
