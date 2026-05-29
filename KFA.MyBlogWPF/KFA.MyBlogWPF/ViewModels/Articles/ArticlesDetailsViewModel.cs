using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels.Articles
{
    public class ArticlesDetailsViewModel : ViewModelBase
    {
        private SelectedArticleStore selectedArticleStore;

        public ArticlesDetailsViewModel(SelectedArticleStore selectedArticleStore)
        {
            this.selectedArticleStore = selectedArticleStore;
        }
    }
}
