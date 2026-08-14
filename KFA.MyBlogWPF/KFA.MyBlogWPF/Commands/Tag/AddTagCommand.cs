using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Tag
{
    public class AddTagCommand : AsyncCommandBase
    {
        private readonly AddTagViewModel _addTagViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;
        private readonly IApiClient _apiClient;
        private readonly ITagService _tagService;
        public AddTagCommand(AddTagViewModel addTagViewModel, 
                             ModalNavigationStore modalNavigationStore, 
                             TagsStore tagsStore, 
                             IApiClient apiClient, 
                             ITagService tagService)
        {
            _addTagViewModel = addTagViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
            _apiClient = apiClient;
            _tagService = tagService;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            TagDetailsFormViewModel formViewModel = _addTagViewModel.TagDetailsFormViewModel;
            var tagName = formViewModel.TagName;

            // Send API request to Edit Tag
            try
            {
                var tag = await _tagService.AddTagAsync(tagName);

                await _tagsStore.Add(tag);
            }
            catch (ValidationException ex)
            {
                Debug.WriteLine($"❌ Ошибка валидации при добавлении тега: {ex.Message}");
                _addTagViewModel.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Исключение при добавлении тега: {ex.Message}");
                _addTagViewModel.ErrorMessage = $"❌ Исключение при добавлении тега: {ex.Message}";
            }
            finally
            {
                _modalNavigationStore.Close();
            }
        }
    }
}
