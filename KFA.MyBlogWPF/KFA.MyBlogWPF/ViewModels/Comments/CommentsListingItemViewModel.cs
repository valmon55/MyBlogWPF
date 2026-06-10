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
        public Comment Comment { get; private set; }
        private readonly int _articleId;
        private readonly CommentsStore _commentsStore;

        public CommentsListingItemViewModel(int articleId, Comment comment, ModalNavigationStore modalNavigationStore, CommentsStore commentsStore)
        {
            Comment = comment;
            _articleId = articleId;
            //EditCommand = new OpenEditCommentCommand()
            //DeleteCommand = new DeleteCommentCommand()
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
        public int Id => Comment.Id;
        public string CommentText => Comment.Comment_Text;
        public string CommentDate => Comment.CommentDate.ToShortDateString();
        public string UserName => Comment?.User.First_Name + " " + Comment?.User.Last_Name;
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public void Update(Comment comment)
        {
            Comment = comment;
            OnPropertyChanged(nameof(CommentText));
            OnPropertyChanged(nameof(CommentDate));
        }
    }
}
