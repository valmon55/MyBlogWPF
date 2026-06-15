using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Comments;
using Model = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Comment
{
    public class EditCommentCommand : AsyncCommandBase
    {
        private EditCommentViewModel _editCommentViewModel;
        private ModalNavigationStore _modalNavigationStore;
        private CommentsStore _commentsStore;

        public EditCommentCommand(EditCommentViewModel editCommentViewModel, ModalNavigationStore modalNavigationStore, CommentsStore commentsStore)
        {
            _editCommentViewModel = editCommentViewModel;
            _modalNavigationStore = modalNavigationStore;
            _commentsStore = commentsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            CommentDetailsFormViewModel formViewModel = _editCommentViewModel.CommentDetailsFormViewModel;
            Model.Comment comment = new Model.Comment()
            {
                Id = _editCommentViewModel.CommentId,
                ArticleId = _editCommentViewModel.ArticleId,
                Comment_Text = formViewModel.CommentText,
                CommentDate = DateTime.Now
            };
            try
            {
                await _commentsStore.Update(comment);
                _modalNavigationStore.Close();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
