using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    /// <summary>
    /// Для представления TagsView
    /// Содержит только команду для добавления тега
    /// </summary>
    public class TagsViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        public TagsListingViewModel TagsListingViewModel { get; }
        public ICommand AddTagsCommand { get; }
        public TagsViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore)
        {
            _myBlog = myBlog;
            TagsListingViewModel = new TagsListingViewModel(_myBlog);

            AddTagsCommand = new OpenAddTagCommand(modalNavigationStore);
        }
    }
}
