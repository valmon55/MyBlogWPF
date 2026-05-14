using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Models
{
    public class User
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Login { get; set; }
        public List<Role> Roles { get; set; }

    }
}
