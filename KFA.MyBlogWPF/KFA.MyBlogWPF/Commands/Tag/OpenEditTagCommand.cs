using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFA.MyBlogWPF.ViewModels.Tags;
using KFA.MyBlogWPF.Services;

namespace KFA.MyBlogWPF.Commands.Tag
{
    public class OpenEditTagCommand : CommandBase
    {
        private readonly TagsListingItemViewModel _tagsListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;
        private readonly IApiClient _apiClient;
        private readonly ITagService _tagService;

        public OpenEditTagCommand(TagsListingItemViewModel tagsListingItemViewModel, 
            ModalNavigationStore modalNavigationStore, 
            TagsStore tagsStore, 
            IApiClient apiClient, 
            ITagService tagService)
        {
            _tagsListingItemViewModel = tagsListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
            _apiClient = apiClient;
            _tagService = tagService;
        }

        public override void Execute(object? parameter)
        {
            Model.Tag tag = _tagsListingItemViewModel.Tag;

            EditTagViewModel editTagViewModel = new EditTagViewModel(tag, _tagsStore, _modalNavigationStore, _apiClient, _tagService);
            _modalNavigationStore.CurrentViewModel = editTagViewModel;
        }

    }
}
