using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Articles;
using KFA.MyBlogWPF.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Article
{
    public class AddArticleCommand : AsyncCommandBase
    {
        private readonly AddArticleViewModel _addArticleViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ArticlesStore _articleStore;
        private readonly IApiClient _apiClient;
        private readonly IArticleService _articleService;

        public AddArticleCommand(AddArticleViewModel addArticleViewModel, ModalNavigationStore modalNavigationStore, ArticlesStore articleStore, IApiClient apiClient, IArticleService articleService)
        {
            _addArticleViewModel = addArticleViewModel;
            _modalNavigationStore = modalNavigationStore;
            _articleStore = articleStore;
            _apiClient = apiClient;
            _articleService = articleService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ArticleDetailsFormViewModel formViewModel = _addArticleViewModel.ArticleDetailsFormViewModel;
            Models.Article article = new Models.Article()
            {
                Title = formViewModel.Title,
                Content = formViewModel.Content,
                ArticleDate = formViewModel.ArticleDate,
                Tags = formViewModel.GetSelectedTags()
            };
            try
            {
                if (string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.Content))
                {
                    _addArticleViewModel.ErrorMessage = "Заголовок или сщдержимое статьи не может быть пустым";
                    return;
                }

                _addArticleViewModel.ErrorMessage = null;
                _addArticleViewModel.IsLoading = true;

                var success = await _articleService.AddArticleAsync(article);
                if (!success)
                {
                    _addArticleViewModel.ErrorMessage = "Не удалось добавить статьи на сервере";
                }

            }
            catch (ValidationException ex)
            {
                Debug.WriteLine($"❌ Ошибка валидации при добавлении статьи: {ex.Message}");
                _addArticleViewModel.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Исключение при добавлении статьи: {ex.Message}");
                _addArticleViewModel.ErrorMessage = $"❌ Исключение при добавлении статьи: {ex.Message}";
            }
            finally
            {
                _addArticleViewModel.IsLoading = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
