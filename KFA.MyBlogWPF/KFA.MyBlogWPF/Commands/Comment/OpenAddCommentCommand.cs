using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Comment
{
    public class OpenAddCommentCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CommentsStore _commentsStore;
        private readonly int? _articleId;

        public OpenAddCommentCommand(ModalNavigationStore modalNavigationStore, 
                                    CommentsStore commentsStore,
                                    int? articleId)
        {
            _modalNavigationStore = modalNavigationStore;
            _commentsStore = commentsStore;
            _articleId = articleId;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return _articleId.HasValue;
        }
        public override void Execute(object parameter)
        {
            if (_articleId.HasValue)
            {
                AddCommentViewModel addCommentViewModel = new AddCommentViewModel(
                    _articleId.Value, _modalNavigationStore, _commentsStore);
                _modalNavigationStore.CurrentViewModel = addCommentViewModel;
            }
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
