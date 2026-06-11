using KFA.MyBlogWPF.Commands.Comment;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticlesDetailsViewModel : ViewModelBase
    {
        private readonly SelectedArticleStore _selectedArticleStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CommentsStore _commentsStore;
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

        private CommentsListingViewModel _commentsListingViewModel;
        public CommentsListingViewModel CommentsListingViewModel
        {
            get { return _commentsListingViewModel; }
            set
            {
                _commentsListingViewModel = value;
                OnPropertyChanged(nameof(CommentsListingViewModel));
            }
        }
        public ICommand AddCommentCommand { get; private set; }
        public ArticlesDetailsViewModel(SelectedArticleStore selectedArticleStore, 
                                        ModalNavigationStore modalNavigationStore,
                                        CommentsStore commentsStore)
        {
            _selectedArticleStore = selectedArticleStore;
            _modalNavigationStore = modalNavigationStore;
            _commentsStore = commentsStore;
            //_commentsListingViewModel = new CommentsListingViewModel(SelectedArticle.Id, modalNavigationStore, commentsStore);

            _selectedArticleStore.SelectedArticleChanged += SelectedArticleStore_SelectedArticleChanged;

            _commentsStore.CommentAdded += CommentsStore_CommentAdded;

            //AddCommentCommand = new OpenAddCommentCommand(modalNavigationStore, commentsStore, SelectedArticle?.Id);
        }

        private void CommentsStore_CommentAdded(Comment comment)
        {
            if (HasSelectedArticle != null)
            {
                _commentsListingViewModel.CommentsStore_CommentAdded(comment);
            }
        }

        protected override void Dispose()
        {
            _selectedArticleStore.SelectedArticleChanged -= SelectedArticleStore_SelectedArticleChanged;
            _commentsStore.CommentAdded -= CommentsStore_CommentAdded;
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
            if (HasSelectedArticle)
            {
                _commentsListingViewModel = new CommentsListingViewModel(SelectedArticle.Id, _modalNavigationStore, _commentsStore);
                AddCommentCommand = new OpenAddCommentCommand(_modalNavigationStore, _commentsStore, SelectedArticle.Id);
            }
        }
        public void LoadCommentsForArticle(int articleId)
        {
            CommentsListingViewModel = new CommentsListingViewModel(articleId, _modalNavigationStore, _commentsStore);
        }
    }
}
