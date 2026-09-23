using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class UsersRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("first_Name")]
        public string First_Name { get; set; }
        [JsonPropertyName("last_name")]
        public string Last_Name { get; set; }
        [JsonPropertyName("middle_Name")]
        public string Middle_Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("year")]
        public int year { get; set; }
        [JsonPropertyName("month")]
        public int month { get; set; }
        [JsonPropertyName("day")]
        public int day { get; set; }
        public DateTime BirthDate => new DateTime(year, month, day);
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("userRoles")]
        public List<Role> Roles { get; set; }

    }
}
