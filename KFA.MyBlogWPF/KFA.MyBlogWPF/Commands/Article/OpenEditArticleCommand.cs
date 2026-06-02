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
    public class OpenEditArticleCommand : AsyncCommandBase
    {
        private readonly ArticlesListingItemViewModel _articlesListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ArticlesStore _articlesStore;
        private readonly List<Model.Tag> _allTags;

        public OpenEditArticleCommand(ArticlesListingItemViewModel articlesListingItemViewModel, 
                                      ModalNavigationStore modalNavigationStore, 
                                      ArticlesStore articlesStore,
                                      List<Model.Tag> allTags)
        {
            _articlesListingItemViewModel = articlesListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _articlesStore = articlesStore;
            _allTags = allTags;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Model.Article article = _articlesListingItemViewModel.Article;
            EditArticleViewModel editArticleViewModel = 
                new EditArticleViewModel(article, _articlesStore, _modalNavigationStore, _allTags);
            _modalNavigationStore.CurrentViewModel = editArticleViewModel;
        }
    }
}
