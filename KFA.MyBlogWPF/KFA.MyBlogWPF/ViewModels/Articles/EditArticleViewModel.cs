using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Article;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class EditArticleViewModel : ViewModelBase
    {
        public int ArticleId { get; }
        public ArticleDetailsFormViewModel ArticleDetailsFormViewModel { get; }

        public EditArticleViewModel(Article article, ArticlesStore articlesStore,
                                    ModalNavigationStore modalNavigationStore, List<Tag> allTags)
        {
            ArticleId = article.Id;
            ICommand submitCommand = new EditArticleCommand(this, modalNavigationStore, articlesStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            ArticleDetailsFormViewModel = new ArticleDetailsFormViewModel(submitCommand, cancelCommand)
            {
                Title = article.Title,
                Content = article.Content,
                ArticleDate = article.ArticleDate,
                AuthorId = article.UserId,
                ShortName = article.User.Last_Name + " " + article.User.First_Name,
            };
            ArticleDetailsFormViewModel.InitTags(allTags, article.Tags);
        }
    }
}
