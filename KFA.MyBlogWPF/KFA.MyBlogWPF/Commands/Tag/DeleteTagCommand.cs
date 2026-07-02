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

namespace KFA.MyBlogWPF.Commands.Tag
{
    public class DeleteTagCommand : AsyncCommandBase
    {
        private readonly TagsListingItemViewModel _tagsListingItemViewModel;
        private readonly TagsStore _tagsStore;
        private readonly IApiClient _apiClient;

        public DeleteTagCommand(TagsListingItemViewModel tagsListingItemViewModel, TagsStore tagsStore, IApiClient apiClient)
        {
            _tagsListingItemViewModel = tagsListingItemViewModel;
            _tagsStore = tagsStore;
            _apiClient = apiClient;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _tagsListingItemViewModel.ErrorMessage = null;
            _tagsListingItemViewModel.IsDeleting = true;

            Model.Tag tag = _tagsListingItemViewModel.Tag;
            try
            {
                var endpoint = $"Tag/DeleteTag?id={tag.Id}";
                var response = await _apiClient.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    await _tagsStore.Delete(tag.Id);
                    Debug.WriteLine($"✅ Тег '{tag.Name}' (ID: {tag.Id}) удален");
                }
                else
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    _tagsListingItemViewModel.ErrorMessage =
                        $"Ошибка удаления: {response.StatusCode} - {errorBody}";

                    Debug.WriteLine($"❌ Ошибка удаления: {response.StatusCode} - {errorBody}");
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
