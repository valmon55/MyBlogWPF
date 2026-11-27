using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class ArticleResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ArticleDate { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public List<Tag> Tags { get; set; }

    }
}
