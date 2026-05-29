using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticlesDetailsViewModel : ViewModelBase
    {
        private readonly SelectedArticleStore _selectedArticleStore;
        private Article SelectedArticle => _selectedArticleStore.SelectedArticle;
        public bool HasSelectedArticle => SelectedArticle != null;
        public string Title => SelectedArticle?.Title;
        public string Content => SelectedArticle?.Content;
        public string ArticleDate => SelectedArticle?.ArticleDate.ToShortDateString();
        public string AuthorId => SelectedArticle?.UserId;
        public User Author => SelectedArticle?.User;
        public string ShortName => SelectedArticle?.User.Last_Name + " " + SelectedArticle?.User.First_Name;
        public IEnumerable<Tag> Tags => SelectedArticle?.Tags;
        public IEnumerable<Comment> Comments => SelectedArticle?.Comments;


        public ArticlesDetailsViewModel(SelectedArticleStore selectedArticleStore)
        {
            _selectedArticleStore = selectedArticleStore;
            _selectedArticleStore.SelectedArticleChanged += SelectedArticleStore_SelectedArticleChanged;
        }

        protected override void Dispose()
        {
            _selectedArticleStore.SelectedArticleChanged -= SelectedArticleStore_SelectedArticleChanged;
            base.Dispose();
        }
        private void SelectedArticleStore_SelectedArticleChanged()
        {
            OnPropertyChanged(nameof(HasSelectedArticle));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Content));
            OnPropertyChanged(nameof(ArticleDate));
            OnPropertyChanged(nameof(AuthorId));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Tags));
            OnPropertyChanged(nameof(Comments));
        }
    }
}
