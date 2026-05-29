using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Tags;
using KFA.MyBlogWPF.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticlesListingViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedArticleStore _selectedArticleStore;
        private readonly ArticleStore _articleStore;
        private List<Tag> _allTags;
        private List<Comment> _allComments;

        private ObservableCollection<ArticlesListingItemViewModel> _articlessListingItemViewModels;
        public IEnumerable<ArticlesListingItemViewModel> ArticlesListingItemViewModels => _articlessListingItemViewModels;

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
        public ArticlesListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore, SelectedArticleStore selectedArticleStore, ArticleStore articleStore)
        {
            _myBlog = myBlog;
            _modalNavigationStore = modalNavigationStore;
            _selectedArticleStore = selectedArticleStore;
            _articleStore = articleStore;

            _articleStore.ArticleAdded += ArticleStore_ArticleAdded;
            _articleStore.ArticleUpdated += ArticleStore_ArticleUpdated;
            _articleStore.ArticleDeleted += ArticleStore_ArticleDeleted;

            var allTags = new List<Tag>()
            {
                new Tag() { Id = 1, Name = "C#"},
                new Tag() { Id = 2, Name = "ASP" },
                new Tag() { Id = 3, Name = "Xamarin" }
            };
            _allTags = allTags;
            _articlessListingItemViewModels = new ObservableCollection<ArticlesListingItemViewModel>();
            _articlessListingItemViewModels.Add(
                new ArticlesListingItemViewModel(
                    new Article()
                    {
                        Id = 1,
                        Title = "C# Language",
                        ArticleDate = DateTime.Now,
                        Content = "C# is Modern and high performance Language",
                        UserId = "1",
                        User = new User()
                        {
                            Id = 1,
                            Login = "Admin"
                        },
                        Tags = new List<Tag>()
                        {
                            new Tag() { Id = 1, Name = "C#" },
                            new Tag() { Id = 2, Name = "ASP" }
                            }
                    },
                    allTags,
                    modalNavigationStore
                    )
                );
            _articlessListingItemViewModels.Add(
                new ArticlesListingItemViewModel(
                    new Article()
                    {
                        Id = 1,
                        Title = "JavaScript",
                        ArticleDate = DateTime.Now,
                        Content = "JS is Modern Language for Web",
                        UserId = "2",
                        User = new User()
                        {
                            Id = 2,
                            Login = "a"
                        },
                        Tags = new List<Tag>()
                        {
                            new Tag() { Id = 1, Name = "JS" },
                            new Tag() { Id = 2, Name = "ASP" }
                            }
                    },
                    allTags,
                    modalNavigationStore
                    )
                );
            _articlessListingItemViewModels.Add(
                new ArticlesListingItemViewModel(
                    new Article()
                    {
                        Id = 1,
                        Title = "C# Xamarin",
                        ArticleDate = DateTime.Now,
                        Content = "Xamarin platform is Obsolet",
                        UserId = "1",
                        User = new User()
                        {
                            Id = 1,
                            Login = "Admin"
                        },
                        Tags = new List<Tag>()
                        {
                            new Tag() { Id = 1, Name = "C#" },
                            new Tag() { Id = 2, Name = "Xamarin" }
                            }
                    },
                    allTags,
                    modalNavigationStore
                    )
                );

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
                _articlessListingItemViewModels.FirstOrDefault(x => x.Article.Id == Id);
            if (articleViewModel != null)
            {
                _articlessListingItemViewModels.Add(articleViewModel);
            }
        }

        private void ArticleStore_ArticleUpdated(Article article)
        {
            ArticlesListingItemViewModel articleViewModel =
                _articlessListingItemViewModels.FirstOrDefault(x => x.Article.Id == article.Id);
            if(articleViewModel != null)
            {
                //articleViewModel.Update(article);
            }
        }

        private void ArticleStore_ArticleAdded(Article article)
        {
            //ArticlesListingItemViewModel articleViewModel =
            //     new ArticlesListingItemViewModel(article, _modalNavigationStore, _articlesStore);
            //_articlesListingItemViewModels.Add(articleViewModel);
        }
    }
}
