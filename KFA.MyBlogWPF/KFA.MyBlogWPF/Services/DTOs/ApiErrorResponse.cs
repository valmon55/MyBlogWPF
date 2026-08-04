using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class ApiErrorResponse
    {
        public string? Message { get; set; }
        public string? Details { get; set; }
        public int? Code { get; set; }
    }
}
