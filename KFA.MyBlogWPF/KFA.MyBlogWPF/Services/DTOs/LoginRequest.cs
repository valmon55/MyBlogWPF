using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class LoginRequest
    {
        [JsonPropertyName("email")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
