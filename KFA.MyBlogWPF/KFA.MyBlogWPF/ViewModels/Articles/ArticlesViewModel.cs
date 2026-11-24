using System.Windows.Input;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.Commands.Article;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Services;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticlesViewModel : ViewModelBase
    {
        public ArticlesListingViewModel ArticlesListingViewModel { get; }
        public ArticlesDetailsViewModel ArticlesDetailsViewModel { get; }
        public ICommand AddArticlesCommand { get; }
        public ArticlesViewModel(IApiClient apiClient, 
                                ApiSettings apiSettings,   
                                AppSettings appSettings,
                                FeatureFlags featureFlags,
                                ModalNavigationStore modalNavigationStore,
                                SelectedArticleStore selectedArticleStore,
                                ArticlesStore articleStore,
                                CommentsStore commentsStore,
                                IArticleService articleService,
                                ITagService tagService)
        {
            ArticlesListingViewModel = new ArticlesListingViewModel(
                                            apiClient,apiSettings, appSettings, featureFlags, 
                                            modalNavigationStore, selectedArticleStore,articleStore,
                                            articleService, tagService);
            ArticlesDetailsViewModel = new ArticlesDetailsViewModel(selectedArticleStore, modalNavigationStore, commentsStore);
            
            List<Tag> allTags = new List<Tag>()
            {
                new Tag() { Id = 1, Name = "C#"},
                new Tag() { Id = 2, Name = "ASP"},
                new Tag() { Id = 3, Name = "JavaScript"},
                new Tag() { Id = 4, Name = "WPF"}
            };

            AddArticlesCommand = new OpenAddArticleCommand(modalNavigationStore, articleStore, allTags);
        }
    }
}
