using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels.Comments
{
    public class CommentsListingViewModel : ViewModelBase
    {
        private int _articleId;
        private ModalNavigationStore _modalNavigationStore;
        private CommentsStore _commentsStore;

        private ObservableCollection<Comment> comments;
        public ObservableCollection<Comment> Comments
        {
            get { return comments;}
            set
            {
                comments = value;
                OnPropertyChanged();
            }
        }
        private readonly ObservableCollection<CommentsListingItemViewModel> _commentsListingItemViewModels;
        public IEnumerable<CommentsListingItemViewModel> CommentsListingItemViewModels => _commentsListingItemViewModels;
        public CommentsListingViewModel(int articleId, ModalNavigationStore modalNavigationStore, CommentsStore commentsStore)
        {
            _articleId = articleId;
            _modalNavigationStore = modalNavigationStore;
            _commentsStore = commentsStore;
            ///Пока так
            ///TODO: при интеграции запрашивать список комментариев 
            _commentsListingItemViewModels = new ObservableCollection<CommentsListingItemViewModel>();
            comments = new ObservableCollection<Comment>();

            _commentsStore.CommentAdded += CommentsStore_CommentAdded;
            _commentsStore.CommentUpdated += CommentsStore_CommentUpdated;
            _commentsStore.CommentDeleted += CommentsStore_CommentDeleted;

            LoadComments();
        }
        private void LoadComments()
        {
            var comments = new List<Comment>()
            {
                new Comment(){ Id = 1, ArticleId = 1, CommentDate = DateTime.Now, Comment_Text = "Comment 1" },
                new Comment(){ Id = 2, ArticleId = 1, CommentDate = DateTime.Now, Comment_Text = "Comment 2" }
            };
            foreach(var comment in comments)
            {
                AddComment(comment);
            }
        }

        protected override void Dispose()
        {
            _commentsStore.CommentAdded -= CommentsStore_CommentAdded;
            _commentsStore.CommentUpdated -= CommentsStore_CommentUpdated;
            _commentsStore.CommentDeleted -= CommentsStore_CommentDeleted;

            base.Dispose();
        }
        private void CommentsStore_CommentDeleted(int commentId)
        {
            CommentsListingItemViewModel commentViewModel = 
                _commentsListingItemViewModels.FirstOrDefault(x => x.Id == commentId);
        }

        private void CommentsStore_CommentUpdated(Comment comment)
        {
            CommentsListingItemViewModel? commentViewModel = 
                _commentsListingItemViewModels.FirstOrDefault(x => x.Id == comment.Id);
            if(commentViewModel != null)
            {
                commentViewModel.Update(comment);
            }
        }

        public void CommentsStore_CommentAdded(Comment comment)
        {
            AddComment(comment);
        }

        private void AddComment(Comment comment)
        {
            CommentsListingItemViewModel itemViewModel = new CommentsListingItemViewModel(_articleId, comment, _modalNavigationStore, _commentsStore);
            _commentsListingItemViewModels.Add(itemViewModel);
        }
    }
}
