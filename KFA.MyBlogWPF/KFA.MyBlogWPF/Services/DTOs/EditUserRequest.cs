using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class EditUserRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("first_Name")]
        public string First_Name { get; set; }
        [JsonPropertyName("last_Name")]
        public string Last_Name { get; set; }
        [JsonPropertyName("middle_Name")]
        public string Middle_Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("birthDate")]
        public string BirthDate { get; set; } // "2026-09-25"
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("userRoles")]
        public List<Role> Roles { get; set; }

    }
}
