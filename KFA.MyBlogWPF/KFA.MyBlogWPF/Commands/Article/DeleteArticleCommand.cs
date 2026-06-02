using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Articles;
using Model = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Article
{
    public class DeleteArticleCommand : AsyncCommandBase
    {
        private readonly ArticlesListingItemViewModel _articlesListingItemViewModel;
        private readonly ArticleStore _articleStore;

        public DeleteArticleCommand(ArticlesListingItemViewModel articlesListingItemViewModel, ArticleStore articleStore)
        {
            _articlesListingItemViewModel = articlesListingItemViewModel;
            _articleStore = articleStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _articlesListingItemViewModel.ErrorMessage = null;
            _articlesListingItemViewModel.IsDeleting = true;
            Model.Article article = _articlesListingItemViewModel.Article;
            try
            {
                await _articleStore.Delete(article.Id);
            }
            catch(Exception ex)
            {
                _articlesListingItemViewModel.ErrorMessage = $"Failed to delete article {article.Title}";
            }
            finally
            {
                _articlesListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
