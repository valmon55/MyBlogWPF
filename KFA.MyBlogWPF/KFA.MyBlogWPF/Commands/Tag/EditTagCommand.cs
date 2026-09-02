using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Tag
{
    public class EditTagCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;
        private readonly EditTagViewModel _editTagViewModel;
        private readonly IApiClient _apiClient;
        private readonly ITagService _tagService;

        public EditTagCommand(EditTagViewModel editTagViewModel, 
            ModalNavigationStore modalNavigationStore, 
            TagsStore tagsStore, 
            IApiClient apiClient, 
            ITagService tagService)
        {
            _editTagViewModel = editTagViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
            _apiClient = apiClient;
            _tagService = tagService;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            // Send API request to Add Tag
            try
            {
                TagDetailsFormViewModel formViewModel = _editTagViewModel.TagDetailsFormViewModel;
                Models.Tag tag = new Models.Tag()
                {
                    Id = _editTagViewModel.TagId,
                    Name = formViewModel.TagName
                };
                if (string.IsNullOrEmpty(tag.Name))
                {
                    _editTagViewModel.ErrorMessage = "Имя тега не может быть пустым";
                    return;
                }

                _editTagViewModel.ErrorMessage = null;
                _editTagViewModel.IsLoading = true;

                var success = await _tagService.UpdateTagAsync(tag);
                if (!success)
                {
                    _editTagViewModel.ErrorMessage = "Не удалось добавить тег на сервере";
                }
                
            }
            catch (ValidationException ex)
            {
                Debug.WriteLine($"❌ Ошибка валидации при добавлении тега: {ex.Message}");
                _editTagViewModel.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Исключение при добавлении тега: {ex.Message}");
                _editTagViewModel.ErrorMessage = $"❌ Исключение при добавлении тега: {ex.Message}";
            }
            finally
            {
                _editTagViewModel.IsLoading = false;
                _modalNavigationStore.Close();
            }
        }

    }
}
