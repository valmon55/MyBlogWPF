using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Articles;
using KFA.MyBlogWPF.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Article
{
    public class AddArticleCommand : AsyncCommandBase
    {
        private AddArticleViewModel _addArticleViewModel;
        private ModalNavigationStore _modalNavigationStore;
        private ArticlesStore _articleStore;

        public AddArticleCommand(AddArticleViewModel addArticleViewModel, ModalNavigationStore modalNavigationStore, ArticlesStore articleStore)
        {
            _addArticleViewModel = addArticleViewModel;
            _modalNavigationStore = modalNavigationStore;
            _articleStore = articleStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Random random = new Random();
            int n = random.Next(1, 100);

            ArticleDetailsFormViewModel formViewModel = _addArticleViewModel.ArticleDetailsFormViewModel;
            Models.Article article = new Models.Article()
            {
                Id = n,
                Title = formViewModel.Title,
                Content = formViewModel.Content,
                ArticleDate = formViewModel.ArticleDate,
                UserId = formViewModel.AuthorId,
                Tags = formViewModel.GetSelectedTags()
            };
            // Send API request to Add Article

            try
            {
                await _articleStore.Add(article);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
