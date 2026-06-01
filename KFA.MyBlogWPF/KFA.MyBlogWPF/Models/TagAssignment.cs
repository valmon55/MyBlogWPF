using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Models
{
    public class TagAssignment
    {
        public Tag Tag { get; set; }
        public bool IsAssigned { get; set; }
        public string Name => Tag?.Name;
    }
}
