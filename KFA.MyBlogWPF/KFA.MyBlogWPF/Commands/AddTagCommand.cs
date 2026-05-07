using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands
{
    public class AddTagCommand : AsyncCommandBase
    {
        private readonly AddTagViewModel _addTagViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;

        public AddTagCommand(AddTagViewModel addTagViewModel, ModalNavigationStore modalNavigationStore, TagsStore tagsStore)
        {
            _addTagViewModel = addTagViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            TagDetailsFormViewModel formViewModel = _addTagViewModel.TagDetailsFormViewModel;
            Models.Tag tag = new Models.Tag() { Name = formViewModel.TagName };
            // Send API request to Edit Tag

            try
            {
                await _tagsStore.Add(tag);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
