using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class AddArticleRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("articleDate")]
        public string ArticleDate { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("tags")]
        public List<Tag> Tags { get; set; }


    }
}
