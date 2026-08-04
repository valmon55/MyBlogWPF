using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Tags;
using System;
using System.Collections.Generic;
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

        public AddTagCommand(AddTagViewModel addTagViewModel, ModalNavigationStore modalNavigationStore, TagsStore tagsStore, IApiClient apiClient)
        {
            _addTagViewModel = addTagViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
            _apiClient = apiClient;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Random random = new Random();
            int n = random.Next(1, 100);

            TagDetailsFormViewModel formViewModel = _addTagViewModel.TagDetailsFormViewModel;
            var tagName = formViewModel.TagName;
            
            Models.Tag pendingTag = new Models.Tag()
            {
                Id = n,
                Name = formViewModel.TagName
            };
            // Send API request to Edit Tag

            try
            {
                const string endpoint = "Tag/AddTag";

                var request = new AddTagRequest() { Name = tagName };

                var responseMessage = await _apiClient.PostAsync<AddTagRequest>(endpoint, request);
                if (responseMessage.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"✅ Тег {request.Name} успешно добавлен");
                    await _tagsStore.Add(pendingTag);

                    var responseBody = await responseMessage.Content.ReadAsStringAsync();
                    Debug.WriteLine($"BODY {responseBody}");
                }
                else
                {
                    var errorBody = await responseMessage.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Ошибка добавления тега: {responseMessage.StatusCode}");
                    Debug.WriteLine($"📄 Тело ответа: {errorBody}");
                }

                _modalNavigationStore.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Исключение при добавлении тега: {ex.Message}");
            }
            //try
            //{
            //    await _tagsStore.Add(tag);

            //    _modalNavigationStore.Close();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }
        }
}
