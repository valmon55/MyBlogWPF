using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Tags;
using Model = KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            Debug.WriteLine($"⚡ AddTagCommand.ExecuteAsync вызван (Thread: {Thread.CurrentThread.ManagedThreadId})");
            try
            {
                TagDetailsFormViewModel formViewModel = _addTagViewModel.TagDetailsFormViewModel;
                var tagName = formViewModel.TagName;
                if (string.IsNullOrEmpty(tagName))
                {
                    _addTagViewModel.ErrorMessage = "Имя тега не может быть пустым";
                    return;
                }

                _addTagViewModel.ErrorMessage = null;
                _addTagViewModel.IsLoading = true;

                var success = await _tagService.AddTagAsync(tagName);
                if (!success)
                {
                    _addTagViewModel.ErrorMessage = "Не удалось добавить тег на сервере";
                }
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
                _addTagViewModel.IsLoading = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
