using KFA.MyBlogWPF.Commands.Article;
using KFA.MyBlogWPF.Commands.User;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticlesListingItemViewModel : ViewModelBase
    {
        private readonly List<Tag> _allTags;
        public Article Article { get; private set; }
        public string Title => Article.Title;
        public DateTime ArticleDate => Article.ArticleDate;
        public IEnumerable<Tag> Tags => Article.Tags;
        public IEnumerable<Comment> Comments => Article.Comments;
        public string UserId => Article.UserId;
        public User User => Article.User;

        private bool isDeleting;
        public bool IsDeleting
        {
            get
            {
                return isDeleting;
            }
            set
            {
                isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return ErrorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ArticlesListingItemViewModel(Article article, List<Tag> allTags, ModalNavigationStore modalNavigationStore, ArticleStore articlesStore)
        {
            Article = article;
            _allTags = allTags;

            //EditCommand = new OpenEditUserCommand(this, modalNavigationStore, usersStore, _allRoles);
            DeleteCommand = new DeleteArticleCommand(this, articlesStore);

        }
    }
}