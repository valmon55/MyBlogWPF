using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Comment
{
    public class OpenEditCommentCommand : AsyncCommandBase
    {
        private readonly CommentsListingItemViewModel _commentsListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CommentsStore _commentsStore;

        public OpenEditCommentCommand(
                CommentsListingItemViewModel commentsListingItemViewModel, 
                ModalNavigationStore modalNavigationStore, 
                CommentsStore commentsStore)
        {
            _commentsListingItemViewModel = commentsListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _commentsStore = commentsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Model.Comment comment = _commentsListingItemViewModel.Comment;

            EditCommentViewModel editCommentViewModel = new EditCommentViewModel(comment, _modalNavigationStore, _commentsStore);
            _modalNavigationStore.CurrentViewModel = editCommentViewModel;
        }
    }
}
