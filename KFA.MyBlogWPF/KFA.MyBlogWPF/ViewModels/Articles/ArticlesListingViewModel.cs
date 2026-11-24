using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Tags;
using KFA.MyBlogWPF.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticlesListingViewModel : ViewModelBase
    {
        private readonly IApiClient _apiClient;
        private readonly ApiSettings _apiSettings;
        private readonly AppSettings _appSettings;
        private readonly FeatureFlags _featureFlags;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedArticleStore _selectedArticleStore;
        private readonly ArticlesStore _articleStore;
        private readonly IArticleService _articleService;
        private readonly ITagService _tagService;
        
        private List<Comment> _allComments;

        private ObservableCollection<ArticlesListingItemViewModel> _articlesListingItemViewModels;
        public IEnumerable<ArticlesListingItemViewModel> ArticlesListingItemViewModels => _articlesListingItemViewModels;

        private ArticlesListingItemViewModel _selectedArticleListingItemViewModel;

        public ArticlesListingItemViewModel SelectedArticleListingItemViewModel
        {
            get
            {
                return _selectedArticleListingItemViewModel;
            }
            set
            {
                _selectedArticleListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedArticleListingItemViewModel));

                _selectedArticleStore.SelectedArticle = _selectedArticleListingItemViewModel?.Article;
            }
        }
        public ArticlesListingViewModel(
            IApiClient apiClient,
            ApiSettings apiSettings,
            AppSettings appSettings,
            FeatureFlags featureFlags,
            ModalNavigationStore modalNavigationStore, 
            SelectedArticleStore selectedArticleStore, 
            ArticlesStore articleStore,
            IArticleService articleService,
            ITagService tagService)
        {
            _apiClient = apiClient;
            _apiSettings = apiSettings;
            _appSettings = appSettings;
            _featureFlags = featureFlags;
            _modalNavigationStore = modalNavigationStore;
            _selectedArticleStore = selectedArticleStore;
            _articleStore = articleStore;
            _articleService = articleService;
            _tagService = tagService;

            _articlesListingItemViewModels = new ObservableCollection<ArticlesListingItemViewModel>();

            _articleStore.ArticleAdded += ArticleStore_ArticleAdded;
            _articleStore.ArticleUpdated += ArticleStore_ArticleUpdated;
            _articleStore.ArticleDeleted += ArticleStore_ArticleDeleted;

            ReloadAllArticlesAsync();
        }

        private async Task ReloadAllArticlesAsync()
        {
            try
            {
                List<Tag> _allTags = await _tagService.GetAllTagAsync();
                var articles = await _articleService.GetAllArticlesAsync();
                _articlesListingItemViewModels.Clear();
                if (articles != null)
                {
                    foreach (var article in articles)
                    {
                        _articlesListingItemViewModels.Add(
                            new ArticlesListingItemViewModel(article, _allTags, _modalNavigationStore, 
                                                            _articleStore, _apiClient, _articleService));
                    }
                    Debug.WriteLine($"🔄 Загружено {articles?.Count ?? 0} статей");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Ошибка перезагрузки статуй: {ex.Message}");
                // Можно показать ошибку пользователю
                ErrorMessage = "Не удалось обновить список статей";
            }
        }

        protected override void Dispose()
        {
            _articleStore.ArticleAdded -= ArticleStore_ArticleAdded;
            _articleStore.ArticleUpdated -= ArticleStore_ArticleUpdated;
            _articleStore.ArticleDeleted -= ArticleStore_ArticleDeleted;

            base.Dispose();
        }

        private void ArticleStore_ArticleDeleted(int Id)
        {
            ArticlesListingItemViewModel articleViewModel =
                _articlesListingItemViewModels.FirstOrDefault(x => x.Article.Id == Id);
            if (articleViewModel != null)
            {
                _articlesListingItemViewModels.Remove(articleViewModel);
            }
        }

        private void ArticleStore_ArticleUpdated(Article article)
        {
            ArticlesListingItemViewModel articleViewModel =
                _articlesListingItemViewModels.FirstOrDefault(x => x.Article.Id == article.Id);
            if(articleViewModel != null)
            {
                articleViewModel.Update(article);
            }
        }

        private void ArticleStore_ArticleAdded(Article article)
        {
            //ArticlesListingItemViewModel articleViewModel =
            //     new ArticlesListingItemViewModel(article, _allTags, _modalNavigationStore, _articleStore);
            //_articlesListingItemViewModels.Add(articleViewModel);
        }
    }
}
