using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Comment_Text { get; set; }
        public DateTime CommentDate { get; set; }
        public int ArticleId { get; set; }
        public string UserId { get; set; }
     }
}
