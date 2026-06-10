using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Comments
{
    public class CommentsViewModel : ViewModelBase
    {
        public CommentsListingViewModel CommentsListingViewModel { get; }
        public ICommand AddComment { get; }
        public CommentsViewModel(SelectedArticleStore selectedArticleStore,
                                ModalNavigationStore modalNavigationStore,
                                CommentsStore commentsStore)
        {
            CommentsListingViewModel = new CommentsListingViewModel(selectedArticleStore.SelectedArticle.Id, 
                                            modalNavigationStore, commentsStore);
            //AddComment = new OpenAddCommentCommand(articleId, modalNavigationStore, commentsStore);
        }


    }
}
