using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Stores
{
    public class SelectedUserStore
    {
		private readonly UsersStore _usersStore;

        public SelectedUserStore(UsersStore usersStore)
        {
            _usersStore = usersStore;
            _usersStore.UserUpdated += UsersStore_UserUpdated;
        }

        private void UsersStore_UserUpdated(User user)
        {
			if (user.Id == SelectedUser?.Id)
			{
				SelectedUser = user;
			}
        }

        private User _selectedUser;
		public User SelectedUser
		{
			get 
			{ 
				return _selectedUser; 
			}
			set 
			{ 
				_selectedUser = value;
				SelectedUserChanged?.Invoke();
			}
		}
		public event Action SelectedUserChanged;
	}
}
