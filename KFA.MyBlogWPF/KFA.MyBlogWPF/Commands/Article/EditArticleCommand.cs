using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Articles;
using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Article
{
    public class EditArticleCommand : AsyncCommandBase
    {
        private readonly EditArticleViewModel _editArticleViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ArticlesStore _articlesStore;

        public EditArticleCommand(EditArticleViewModel editArticleViewModel, ModalNavigationStore modalNavigationStore, ArticlesStore articlesStore)
        {
            _editArticleViewModel = editArticleViewModel;
            _modalNavigationStore = modalNavigationStore;
            _articlesStore = articlesStore;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return _editArticleViewModel.ArticleDetailsFormViewModel.CanSubmit;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            // Send API request to Add User

            ArticleDetailsFormViewModel formViewModel = _editArticleViewModel.ArticleDetailsFormViewModel;

            Model.Article article = new Model.Article()
            {
                Id = _editArticleViewModel.ArticleId,                
                ArticleDate = formViewModel.ArticleDate,
                Title = formViewModel.Title,
                Content = formViewModel.Content,
                UserId = formViewModel.AuthorId,
                Tags = formViewModel.GetSelectedTags(),
            };
            // Send API request to Edit User

            try
            {
                await _articlesStore.Update(article);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
