using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Comments;
using KFA.MyBlogWPF.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Comment
{
    public class DeleteCommentCommand : AsyncCommandBase
    {
        private readonly CommentsListingItemViewModel _commentsListingItemViewModel;
        private readonly CommentsStore _commentsStore;

        public DeleteCommentCommand(CommentsListingItemViewModel commentsListingItemViewModel, 
                CommentsStore commentsStore)
        {
            _commentsListingItemViewModel = commentsListingItemViewModel;
            _commentsStore = commentsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _commentsListingItemViewModel.ErrorMessage = null;
            _commentsListingItemViewModel.IsDeleting = true;
            Models.Comment comment = _commentsListingItemViewModel.Comment;
            try
            {
                await _commentsStore.Delete(comment.Id);
            }
            catch (Exception)
            {
                _commentsListingItemViewModel.ErrorMessage = $"Failed to delete comment {comment.Comment_Text}";
            }
            finally
            {
                _commentsListingItemViewModel.IsDeleting = false;
            }

        }
    }
}
