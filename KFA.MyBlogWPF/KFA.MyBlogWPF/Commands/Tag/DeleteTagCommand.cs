using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KFA.MyBlogWPF.ViewModels.Tags;
using KFA.MyBlogWPF.Services;
using System.Diagnostics;
using KFA.MyBlogWPF.Models;

namespace KFA.MyBlogWPF.Commands.Tag
{
    public class DeleteTagCommand : AsyncCommandBase
    {
        private readonly TagsListingItemViewModel _tagsListingItemViewModel;
        private readonly ITagService _tagService;
        private readonly TagsStore _tagsStore;
        private readonly IApiClient _apiClient;

        public DeleteTagCommand(TagsListingItemViewModel tagsListingItemViewModel, 
            TagsStore tagsStore, IApiClient apiClient, ITagService tagService)
        {
            _tagsListingItemViewModel = tagsListingItemViewModel;
            _tagsStore = tagsStore;
            _apiClient = apiClient;
            _tagService = tagService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _tagsListingItemViewModel.ErrorMessage = null;
            _tagsListingItemViewModel.IsDeleting = true;

            Model.Tag tag = _tagsListingItemViewModel.Tag;
            try
            {
                var success = await _tagService.DeleteTagAsync(tag);
                if (!success) 
                {
                     _tagsListingItemViewModel.ErrorMessage = "Не удалось добавить тег на сервере";
                }
            }
            catch (Exception ex)
            {
                _tagsListingItemViewModel.ErrorMessage = $"Исключение: {ex.Message}";
                Debug.WriteLine($"❌ Исключение при удалении: {ex.Message}");
            }
            finally
            {
                _tagsListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
