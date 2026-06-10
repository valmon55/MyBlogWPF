using KFA.MyBlogWPF.Commands.Comment;
using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Stores
{
    public class CommentsStore
    {
        public event Action<Comment> CommentAdded;
        public event Action<Comment> CommentUpdated;
        public event Action<int> CommentDeleted;
        public async Task Add(Comment comment)
        {
            CommentAdded?.Invoke(comment);
        }
        public async Task Update(Comment comment)
        {
            CommentUpdated?.Invoke(comment);
        }

        public async Task Delete(int id)
        {
            CommentDeleted?.Invoke(id);
        }
    }
}
