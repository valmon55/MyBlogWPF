using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels.Articles;
using Model = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Article
{
    public class OpenAddArticleCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ArticlesStore _articleStore;
        private readonly List<Model.Tag> _allTags;

        public OpenAddArticleCommand(ModalNavigationStore modalNavigationStore, ArticlesStore articleStore, List<Model.Tag> allTags)
        {
            _modalNavigationStore = modalNavigationStore;
            _articleStore = articleStore;
            _allTags = allTags;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            AddArticleViewModel articleViewModel = new AddArticleViewModel(_modalNavigationStore, _articleStore, _allTags);
            _modalNavigationStore.CurrentViewModel = articleViewModel;
        }
    }
}
