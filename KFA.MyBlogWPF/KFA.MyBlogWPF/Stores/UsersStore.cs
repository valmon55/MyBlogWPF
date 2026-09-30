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
        public event Func<User, Task> UserUpdated;
        public event Func<User, Task> UserDeleted;
        public async Task Update(User user)
        {
            UserUpdated?.Invoke(user);
        }
        public async Task Delete(User user)
        {
            UserDeleted?.Invoke(user);
        }
    }
}
