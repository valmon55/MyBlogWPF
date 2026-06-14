using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Comments;
using Models = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public override async Task ExecuteAsync(object parameter)
        {
            Models.Comment comment = new Models.Comment()
            {
                ArticleId = _articleId,
                Comment_Text = _addCommentViewModel.CommentDetailsFormViewModel.CommentText,
                CommentDate = DateTime.Now,
                UserId  = "1", //временно
                User = new Models.User() { 
                    Id = 1, 
                    First_Name = "Fedor",
                    Last_Name = "Kr",
                    Middle_Name = "A",
                    BirthDate = DateTime.Now,
                    Email = "KFA@a.a",
                    Login = "FEDOR"
                }
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
