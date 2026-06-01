using KFA.MyBlogWPF.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using KFA.MyBlogWPF.ViewModels.Articles;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.Commands.Article;
using KFA.MyBlogWPF.Models;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticlesViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;

        public ArticlesListingViewModel ArticlesListingViewModel { get; }
        public ArticlesDetailsViewModel ArticlesDetailsViewModel { get; }
        public ICommand AddArticlesCommand { get; }
        public ArticlesViewModel(HttpClient myBlog, 
                                ModalNavigationStore modalNavigationStore,
                                SelectedArticleStore selectedArticleStore,
                                ArticleStore articleStore)
        {
            _myBlog = myBlog;
            ArticlesListingViewModel = new ArticlesListingViewModel(_myBlog, modalNavigationStore, selectedArticleStore,articleStore);
            ArticlesDetailsViewModel = new ArticlesDetailsViewModel(selectedArticleStore);
            
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
