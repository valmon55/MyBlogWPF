using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KFA.MyBlogWPF.Services
{
    public class TagService : ITagService
    {
        private readonly IApiClient _apiClient;
        private readonly TagsStore _tagsStore;

        public TagService(IApiClient apiClient, TagsStore tagsStore)
        {
            _apiClient = apiClient;
            _tagsStore = tagsStore;
        }

        public async Task<bool> AddTagAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя тега не должно быть пустым", nameof(name));

            const string endpoint = "Tag/AddTag";
            try
            {
                var request = new AddTagRequest() { Name = name.Trim() };

                var response = await _apiClient.PostAsync<AddTagRequest, TagResponse>(endpoint, request);
                if (response.IsSuccess)
                {
                    Debug.WriteLine($"✅ Тег {request.Name} успешно добавлен");
                    var tag = new Tag() { Id = 0, Name = name };
                    await _tagsStore.Add(tag);
                    return true;
                }
                Debug.WriteLine($"Ошибка при добавлении тега {name}");
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception($"❌ Исключение при добавлении тега: {name}");
            }
        }

        public async Task<bool> DeleteTagAsync(string endpoint, Tag tag)
        {
            try
            {
                var response = await _apiClient.DeleteAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"✅ Тег {tag.Name} c id = {tag.Id} удален");
                    await _tagsStore.Delete(tag.Id);
                    return true;
                }
                Debug.WriteLine($"Ошибка при удалении тега {tag.Name}");
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Исключение при удалении тега: {tag.Name}");
            }
        }

        public Task<IReadOnlyList<Tag>> GetAllTagAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTagAsync(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
