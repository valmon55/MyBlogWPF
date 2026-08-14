using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.Commands.Tag
{
    public class OpenAddTagCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;
        private readonly IApiClient _apiClient;
        private readonly ITagService _tagService;

        public OpenAddTagCommand(ModalNavigationStore modalNavigationStore, TagsStore tagsStore, IApiClient apiClient, ITagService tagService)
        {
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
            _apiClient = apiClient;
            _tagService = tagService;
        }

        public override void Execute(object? parameter)
        {
            AddTagViewModel addTagViewModel = new AddTagViewModel(_modalNavigationStore, _tagsStore, _apiClient, _tagService);
            _modalNavigationStore.CurrentViewModel = addTagViewModel;
        }
    }
}
