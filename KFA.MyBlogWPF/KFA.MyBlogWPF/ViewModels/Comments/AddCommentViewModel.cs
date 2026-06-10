using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Comment;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Comments
{
    public class AddCommentViewModel : ViewModelBase
    {
        public CommentDetailsFormViewModel CommentDetailsFormViewModel { get; }
        public AddCommentViewModel(int? articleId,ModalNavigationStore modalNavigationStore, CommentsStore commentsStore)
        {
            ICommand submitCommand = new AddCommentCommand(this, modalNavigationStore, commentsStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            CommentDetailsFormViewModel = new CommentDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
