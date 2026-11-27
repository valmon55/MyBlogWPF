using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Article;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KFA.MyBlogWPF.Services;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class AddArticleViewModel : ViewModelBase
    {
        public Article article = new Article(); 
        public ArticleDetailsFormViewModel ArticleDetailsFormViewModel { get; }

        public AddArticleViewModel(ModalNavigationStore modalNavigationStore, 
            ArticlesStore articleStore, 
            List<Tag> tags,
            IApiClient apiClient,
            IArticleService articleService)
        {
            ICommand submitCommand = new AddArticleCommand(this, modalNavigationStore, articleStore, apiClient, articleService);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            ArticleDetailsFormViewModel = new ArticleDetailsFormViewModel(submitCommand, cancelCommand)
            { 
                //Tags = tags
            };
            ArticleDetailsFormViewModel.InitTags(tags, article.Tags);
        }
    }
}
