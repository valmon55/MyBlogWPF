using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Models
{
    public class RoleAssignment
    {
        public Role Role { get; set; }
        public bool IsAssignment { get; set; }
        public string Name => Role?.Name;
        public string Description => Role?.Description;
    }
}
