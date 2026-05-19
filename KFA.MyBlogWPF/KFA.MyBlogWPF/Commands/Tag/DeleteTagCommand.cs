using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFA.MyBlogWPF.ViewModels.Tags;

namespace KFA.MyBlogWPF.Commands.Tag
{
    public class DeleteTagCommand : AsyncCommandBase
    {
        private readonly TagsListingItemViewModel _tagsListingItemViewModel;
        private readonly TagsStore _tagsStore;

        public DeleteTagCommand(TagsListingItemViewModel tagsListingItemViewModel, TagsStore tagsStore)
        {
            _tagsListingItemViewModel = tagsListingItemViewModel;
            _tagsStore = tagsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _tagsListingItemViewModel.ErrorMessage = null;
            _tagsListingItemViewModel.IsDeleting = true;

            Model.Tag tag = _tagsListingItemViewModel.Tag;
            try
            {
                await _tagsStore.Delete(tag.Id);
            }
            catch (Exception)
            {
                _tagsListingItemViewModel.ErrorMessage = $"Failed to delete Tag {tag.Name}";
            }
            finally
            {
                _tagsListingItemViewModel.IsDeleting = false;
            }

        }
    }
}
