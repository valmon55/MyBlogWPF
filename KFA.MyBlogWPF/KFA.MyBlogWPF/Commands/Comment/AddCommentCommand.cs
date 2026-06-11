using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Comments;
using Models = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Comment
{
    public class AddCommentCommand : AsyncCommandBase
    {
        private readonly AddCommentViewModel _addCommentViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CommentsStore _commentsStore;
        private readonly int _articleId;

        public AddCommentCommand(AddCommentViewModel addCommentViewModel,   
                                ModalNavigationStore modalNavigationStore, 
                                CommentsStore commentsStore, int articleId)
        {
            _addCommentViewModel = addCommentViewModel;
            _modalNavigationStore = modalNavigationStore;
            _commentsStore = commentsStore;
            _articleId = articleId;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Random random = new Random();
            int n = random.Next(1, 100);

            CommentDetailsFormViewModel formViewModel = _addCommentViewModel.CommentDetailsFormViewModel;
            Models.Comment comment = new Models.Comment()
            {
                Id = n,
                ArticleId = _articleId,
                //UserId,
                //CommentDate = formViewModel,
                Comment_Text = formViewModel.CommentText
            };
            // Send API request to Add Role

            try
            {
                await _commentsStore.Add(comment);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
