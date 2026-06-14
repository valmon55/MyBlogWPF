using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Comments
{
    public class CommentsListingItemViewModel : ViewModelBase
    {
        private Comment _comment;
        private readonly int _articleId;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CommentsStore _commentsStore;

        public CommentsListingItemViewModel(int articleId, Comment comment, ModalNavigationStore modalNavigationStore, CommentsStore commentsStore)
        {
            _comment = comment;
            _articleId = articleId;
            _modalNavigationStore = modalNavigationStore;
            _commentsStore = commentsStore;
            //EditCommand = new OpenEditCommentCommand();
            //DeleteCommand = new DeleteCommentCommand();
        }
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
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public int Id => _comment.Id;
        public string CommentText => _comment.Comment_Text;
        public string CommentDate => _comment.CommentDate.ToShortDateString();
        public string UserName => _comment?.User.First_Name + " " + _comment?.User.Last_Name;
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public void Update(Comment comment)
        {
            _comment = comment;
            OnPropertyChanged(nameof(CommentText));
            OnPropertyChanged(nameof(CommentDate));
        }
    }
}
