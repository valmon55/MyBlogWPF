using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Comment;
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
    public class EditCommentViewModel : ViewModelBase
    {
        private readonly Comment _comment;
        public Comment Comment => _comment;
        public int CommentId => _comment.Id;
        public int ArticleId => _comment.ArticleId;
        public CommentDetailsFormViewModel CommentDetailsFormViewModel { get; }
        public EditCommentViewModel(Comment comment, ModalNavigationStore modalNavigationStore, CommentsStore commentsStore)
        {
            _comment = comment;            

            ICommand submitCommand = new EditCommentCommand(this, modalNavigationStore, commentsStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            CommentDetailsFormViewModel = new CommentDetailsFormViewModel(submitCommand, cancelCommand)
            {
                CommentText = comment.Comment_Text,
            };
        }
    }
}
