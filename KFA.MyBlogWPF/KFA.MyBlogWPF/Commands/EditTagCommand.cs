using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands
{
    public class EditTagCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;
        private readonly EditTagViewModel _editTagViewModel;

        public EditTagCommand(EditTagViewModel editTagViewModel, ModalNavigationStore modalNavigationStore, TagsStore tagsStore)
        {
            _editTagViewModel = editTagViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            // Send API request to Add Tag

            TagDetailsFormViewModel formViewModel = _editTagViewModel.TagDetailsFormViewModel;
            Models.Tag tag = new Models.Tag() 
            {
                Id = _editTagViewModel.TagId, 
                Name = formViewModel.TagName 
            };
            // Send API request to Edit Tag

            try
            {
                await _tagsStore.Update(tag);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
