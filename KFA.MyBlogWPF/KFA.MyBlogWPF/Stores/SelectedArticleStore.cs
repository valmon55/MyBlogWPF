using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Stores
{
    public class SelectedArticleStore
    {
        private readonly ArticlesStore _articleStore;

        public SelectedArticleStore(ArticlesStore articleStore)
        {
            _articleStore = articleStore;
            _articleStore.ArticleUpdated += _articleStore_ArticleUpdated;
        }

        private void _articleStore_ArticleUpdated(Article article)
        {
            if(article.Id == SelectedArticle?.Id)
            {
                SelectedArticle = article;
            }
        }
        private Article selectedArticle;
        public Article SelectedArticle
        {
            get 
            {
                return selectedArticle;
            }
            set 
            {
                selectedArticle = value;
                SelectedArticleChanged?.Invoke();
            }
        }
        public event Action SelectedArticleChanged;
    }
}
