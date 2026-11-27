using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Stores
{
    public class ArticlesStore
    {
        public event Func<Article, Task> ArticleAdded;
        public event Func<Article, Task> ArticleUpdated;
        public event Func<int, Task> ArticleDeleted;
        public async Task Add(Article article)
        {
            ArticleAdded?.Invoke(article);
        }
        public async Task Update(Article article)
        {
            ArticleUpdated?.Invoke(article);
        }

        public async Task Delete(int id)
        {
            ArticleDeleted?.Invoke(id);
        }
    }
}

