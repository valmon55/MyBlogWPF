using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class RegisterRequest
    {
        [JsonPropertyName("first_Name")]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("last_Name")]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("middle_Name")]
        public string MiddleName { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("month")]
        public int Month { get; set; }
        [JsonPropertyName("day")]
        public int Day { get; set; }
        [JsonPropertyName("passwordReg")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("passwordConfirm")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [JsonPropertyName("login")] 
        public string Login { get; set; } = string.Empty;
    }
}
