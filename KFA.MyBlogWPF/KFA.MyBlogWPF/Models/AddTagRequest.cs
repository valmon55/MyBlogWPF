using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Models
{
    public class AddTagRequest
    {
        [JsonPropertyName("tag_Name")]
        public string Name { get; set; } = string.Empty;
    }
}
