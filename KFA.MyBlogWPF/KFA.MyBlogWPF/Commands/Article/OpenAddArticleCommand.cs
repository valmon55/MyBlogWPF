using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Articles;
using Model = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFA.MyBlogWPF.Services;

namespace KFA.MyBlogWPF.Commands.Article
{
    public class OpenAddArticleCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ArticlesStore _articleStore;
        private readonly IApiClient _apiClient;
        private readonly IArticleService _articleService;
        private readonly ITagService _tagService;

        public OpenAddArticleCommand(ModalNavigationStore modalNavigationStore,
            ArticlesStore articleStore,
            IApiClient apiClient,
            IArticleService articleService,
            ITagService tagService)
        {
            _modalNavigationStore = modalNavigationStore;
            _articleStore = articleStore;
            _apiClient = apiClient;
            _articleService = articleService;
            _tagService = tagService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var _allTags = await _tagService.GetAllTagAsync();
            AddArticleViewModel articleViewModel = 
                new AddArticleViewModel(_modalNavigationStore, _articleStore, _allTags, _apiClient, _articleService);
            _modalNavigationStore.CurrentViewModel = articleViewModel;
        }
    }
}
