using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KFA.MyBlogWPF.ViewModels.Comments
{
    public class CommentsListingViewModel : ViewModelBase
    {
        private int _articleId;
        private ModalNavigationStore _modalNavigationStore;
        private CommentsStore _commentsStore;

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
            
            //comments = new ObservableCollection<Comment>();

            //_commentsStore.CommentAdded += CommentsStore_CommentAdded;
            //_commentsStore.CommentUpdated += CommentsStore_CommentUpdated;
            //_commentsStore.CommentDeleted += CommentsStore_CommentDeleted;

            //LoadComments();
        }
        public void LoadComments()
        {
            var comments = new List<Comment>()
            {
                new Comment(){ Id = 1, ArticleId = _articleId, CommentDate = DateTime.Now, 
                    Comment_Text = "Comment 1",
                    User = new User(){ Id = 1, First_Name = "Sherlok", Last_Name = "Holms"}
                },
                new Comment(){ Id = 2, ArticleId = _articleId, CommentDate = DateTime.Now, 
                    Comment_Text = "Comment 2", 
                    User = new User(){ Id = 2, First_Name = "Patric", Last_Name = "Jane" }
                }
            };
            foreach(var comment in comments)
            {
                AddComment(comment);
            }
        }

        //protected override void Dispose()
        //{
        //    _commentsStore.CommentAdded -= CommentsStore_CommentAdded;
        //    _commentsStore.CommentUpdated -= CommentsStore_CommentUpdated;
        //    _commentsStore.CommentDeleted -= CommentsStore_CommentDeleted;

        //    base.Dispose();
        //}
        public void DeleteComment(int commentId)
        {
            CommentsListingItemViewModel commentViewModel = 
                _commentsListingItemViewModels.FirstOrDefault(x => x.Id == commentId);
            if (commentViewModel != null)
            {
                _commentsListingItemViewModels.Remove(commentViewModel);
            }
        }

        public void UpdateComment(Comment comment)
        {
            CommentsListingItemViewModel commentViewModel = 
                _commentsListingItemViewModels.FirstOrDefault(x => x.Id == comment.Id);
            if(commentViewModel != null)
            {
                commentViewModel.Update(comment);
            }
        }

        public void AddComment(Comment comment)
        {
            CommentsListingItemViewModel commentViewModel = new CommentsListingItemViewModel(
                _articleId,
                comment,
                _modalNavigationStore,
                _commentsStore
                );
            _commentsListingItemViewModels.Add(commentViewModel);
        }
    }
}
