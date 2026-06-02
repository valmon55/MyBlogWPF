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
        public event Action<Article> ArticleAdded;
        public event Action<Article> ArticleUpdated;
        public event Action<int> ArticleDeleted;
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

