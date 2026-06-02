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
        private readonly ArticlesStore _articleStore;
        private List<Tag> _allTags;
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
        public ArticlesListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore, SelectedArticleStore selectedArticleStore, ArticlesStore articleStore)
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
            _articlesListingItemViewModels = new ObservableCollection<ArticlesListingItemViewModel>();
            _articlesListingItemViewModels.Add(
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
                            First_Name = "A",
                            Middle_Name = "a",
                            Last_Name = "Admin",
                            Login = "Admin"
                        },
                        Tags = new List<Tag>()
                        {
                            new Tag() { Id = 1, Name = "C#" },
                            new Tag() { Id = 2, Name = "ASP" }
                        },
                        Comments = new List<Comment>()
                        {
                            new Comment() {Id = 1, ArticleId = 1, CommentDate = DateTime.Now, Comment_Text = "1st comment", UserId = "2"},
                            new Comment() {Id = 2, ArticleId = 1, CommentDate = DateTime.Now, Comment_Text = "2nd comment", UserId = "1"}
                        }
                    },
                    allTags,
                    modalNavigationStore,
                    articleStore
                    )
                );
            _articlesListingItemViewModels.Add(
                new ArticlesListingItemViewModel(
                    new Article()
                    {
                        Id = 2,
                        Title = "JavaScript",
                        ArticleDate = DateTime.Now,
                        Content = "JS is Modern Language for Web",
                        UserId = "2",
                        User = new User()
                        {
                            Id = 2,
                            First_Name = "Fedor",
                            Middle_Name = "f",
                            Last_Name = "K",
                            Login = "a"
                        },
                        Tags = new List<Tag>()
                        {
                            new Tag() { Id = 1, Name = "JS" },
                            new Tag() { Id = 2, Name = "ASP" }
                        },
                        Comments = new List<Comment>()
                        {
                            new Comment() {Id = 3, ArticleId = 2, CommentDate = DateTime.Now, Comment_Text = "1st comment for JS", UserId = "2"},
                            new Comment() {Id = 4, ArticleId = 2, CommentDate = DateTime.Now, Comment_Text = "2nd comment for JS", UserId = "1"}
                        }
                    },
                    allTags,
                    modalNavigationStore,
                    articleStore
                    )
                );
            _articlesListingItemViewModels.Add(
                new ArticlesListingItemViewModel(
                    new Article()
                    {
                        Id = 3,
                        Title = "C# Xamarin",
                        ArticleDate = DateTime.Now,
                        Content = "Xamarin platform is Obsolet",
                        UserId = "1",
                        User = new User()
                        {
                            Id = 1,
                            First_Name = "Patrick",
                            Middle_Name = "a",
                            Last_Name = "Jane",
                            Login = "Admin"
                        },
                        Tags = new List<Tag>()
                        {
                            new Tag() { Id = 1, Name = "C#" },
                            new Tag() { Id = 2, Name = "Xamarin" }
                        },
                        Comments = new List<Comment>()
                        {
                            new Comment() {Id = 10, ArticleId = 3, CommentDate = DateTime.Now, Comment_Text = "1st comment XAM", UserId = "2"},
                            new Comment() {Id = 11, ArticleId = 3, CommentDate = DateTime.Now, Comment_Text = "2nd comment XAMARIN", UserId = "1"}
                        }
                    },
                    allTags,
                    modalNavigationStore,
                    articleStore
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
            ArticlesListingItemViewModel articleViewModel =
                 new ArticlesListingItemViewModel(article, _allTags, _modalNavigationStore, _articleStore);
            _articlesListingItemViewModels.Add(articleViewModel);
        }
    }
}
