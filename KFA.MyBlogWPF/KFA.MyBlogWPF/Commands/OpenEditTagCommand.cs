using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands
{
    public class OpenEditTagCommand : CommandBase
    {
        private readonly TagsListingItemViewModel _tagsListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;

        public OpenEditTagCommand(TagsListingItemViewModel tagsListingItemViewModel, ModalNavigationStore modalNavigationStore, TagsStore tagsStore)
        {
            _tagsListingItemViewModel = tagsListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
        }

        public override void Execute(object? parameter)
        {
            Tag tag = _tagsListingItemViewModel.Tag;

            EditTagViewModel editTagViewModel = new EditTagViewModel(tag, _tagsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editTagViewModel;
        }

    }
}
