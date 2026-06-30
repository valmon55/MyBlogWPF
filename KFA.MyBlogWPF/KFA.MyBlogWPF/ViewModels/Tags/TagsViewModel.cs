using KFA.MyBlogWPF.Commands.Tag;
using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Tags
{
    /// <summary>
    /// Для представления TagsView
    /// Содержит только команду для добавления тега
    /// </summary>
    public class TagsViewModel : ViewModelBase
    {
        //private readonly HttpClient _myBlog;
        public TagsListingViewModel TagsListingViewModel { get; }
        public ICommand AddTagsCommand { get; }
        public TagsViewModel(//HttpClient myBlog,
            IApiClient apiClient,
            ApiSettings apiSettings,
            AppSettings appSettings,
            FeatureFlags featureFlags,
                            ModalNavigationStore modalNavigationStore,
                            TagsStore tagsStore)
        {
            //_myBlog = myBlog;
            TagsListingViewModel = new TagsListingViewModel(/*_myBlog,*/ 
                apiClient,
                apiSettings,
                appSettings,
                featureFlags,
                modalNavigationStore, tagsStore);

            AddTagsCommand = new OpenAddTagCommand(modalNavigationStore, tagsStore);
        }
    }
}
