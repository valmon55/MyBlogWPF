using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public class TagService : ITagService
    {
        private readonly IApiClient _apiClient;

        public TagService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Tag> AddTagAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя тега не должно быть пустым", nameof(name));

            const string endpoint = "Tag/AddTag";

            var request = new AddTagRequest() { Name = name };

            var response = await _apiClient.PostAsync<AddTagRequest, TagResponse>(endpoint, request);
            if (response == null)
                throw new Exception("Сервер вернул пустой ответ");
            else if (response.IsSuccess == false)
                throw new Exception($"❌ Исключение при добавлении тега: {response.Error.Code} {response.Error.Message}");
            else
            {
                Debug.WriteLine($"✅ Тег {request.Name} успешно добавлен");
                return new Tag() { Id = response.Data.Id, Name = response.Data.Name };
            }
        }

        public Task<bool> DeleteTagAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Tag>> GetAllTagAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> UpdateTagAsync(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
