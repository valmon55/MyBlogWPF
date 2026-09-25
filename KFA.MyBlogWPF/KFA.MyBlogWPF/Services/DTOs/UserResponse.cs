using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Email { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public DateTime BirthDate => new DateTime(year, month, day);
        public string Login { get; set; }
        public List<Role> Roles { get; set; }

    }
}
