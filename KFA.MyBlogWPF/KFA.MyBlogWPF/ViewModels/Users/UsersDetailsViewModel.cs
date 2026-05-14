using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UsersDetailsViewModel : ViewModelBase
    {
        public string FullName { get; }
        public string BirthDate { get; }
        public string Email { get; }
        public string Login { get; }
        public IEnumerable<Role> Roles { get; }
        public UsersDetailsViewModel(/*string fullName, string birthDate, string email, IEnumerable<Role> roles, string login*/)
        {
            //FullName = fullName;
            //BirthDate = birthDate;
            //Email = email;
            //Login = login;
            //Roles = roles;

            FullName = "fullName";
            BirthDate = DateTime.Now.ToLongDateString();
            Email = "email";
            Login = "login";
        }
    }
}
