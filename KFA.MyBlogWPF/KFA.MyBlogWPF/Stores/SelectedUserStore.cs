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
