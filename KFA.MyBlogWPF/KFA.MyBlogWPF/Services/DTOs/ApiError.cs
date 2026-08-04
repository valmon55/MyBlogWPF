using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class ApiError
    {
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }
        public int? Code { get; set; }
    }
}
