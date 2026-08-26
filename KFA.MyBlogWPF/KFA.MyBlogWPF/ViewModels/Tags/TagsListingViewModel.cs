using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Tags
{
    public class TagsListingViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        private readonly IApiClient _apiClient;
        private readonly ApiSettings _apiSettings;
        private readonly AppSettings _appSettings;
        private readonly FeatureFlags _featureFlags;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;

        public string ApplicationName { get; }
        private ObservableCollection<Tag> tags;
        public ObservableCollection<Tag> Tags
        {
            get => tags;
            set
            {
                tags = value;
                OnPropertyChanged();
            }
        }
        private readonly ObservableCollection<TagsListingItemViewModel> _tagsListingItemViewModels;
        public IEnumerable<TagsListingItemViewModel> TagsListingItemViewModels => _tagsListingItemViewModels;
        public TagsListingViewModel(
            IApiClient apiClient,
            ApiSettings apiSettings,
            AppSettings appSettings,
            FeatureFlags featureFlags, 
            ModalNavigationStore modalNavigationStore, 
            TagsStore tagsStore) 
        {
            _apiClient = apiClient;
            _apiSettings = apiSettings;
            _appSettings = appSettings;
            _featureFlags = featureFlags;

            ApplicationName = _appSettings.ApplicationName;

            // Проверяем фичу-флаг
            if (_featureFlags.EnableExperimentalFeatures)
            {
                // Показываем экспериментальный UI
            }

            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
            _tagsListingItemViewModels = new ObservableCollection<TagsListingItemViewModel>();
            Tags = new ObservableCollection<Tag>();

            // 🔍 ЛОГ: проверяем, сколько раз подписываемся
            // 🔍 ЛОГ: проверяем количество подписчиков ДО подписки
            Debug.WriteLine($"📌 ДО подписки: количество подписчиков = {_tagsStore.GetTagAddedSubscriberCount()}");
            Debug.WriteLine($"📌 ДО подписки: {_tagsStore.GetTagAddedSubscribersInfo()}");


            //_tagsStore.TagAdded += TagsStore_TagAdded;
            _tagsStore.TagAdded += OnTagAdded;
            _tagsStore.TagUpdated += TagsStore_TagUpdated;
            _tagsStore.TagDeleted += TagsStore_TagDeleted;

            LoadTagsAsync();
        }

        private async void OnTagAdded(Tag tag)
        {
            // 🔍 ЛОГ: кто вызвал и сколько подписчиков
            Debug.WriteLine($"🔔 OnTagAdded вызван для тега '{tag.Name}'");
            Debug.WriteLine($"📌 Текущее количество подписчиков: {_tagsStore.GetTagAddedSubscriberCount()}");
            Debug.WriteLine($"📌 Список подписчиков: {_tagsStore.GetTagAddedSubscribersInfo()}");

            TagsListingItemViewModel itemViewModel = new TagsListingItemViewModel(
                tag, 
                _modalNavigationStore, 
                _tagsStore, 
                _apiClient
                //,isNew
                );
            _tagsListingItemViewModels.Add(itemViewModel);
            await ReloadAllTagsAsync();

            //try
            //{
            //    var request = new AddTagRequest { Name = tag.Name };
            //    var response = await _apiClient.PostAsync<AddTagRequest, NoContentResponse>("Tag/AddTag", request);

            //    if (response.IsSuccess)
            //    {
            //        // ✅ Успех: перезагружаем ВСЕ теги с сервера
            //        Debug.WriteLine($"✅ Тег '{tag.Name}' добавлен, перезагружаем список...");
            //        await ReloadAllTagsAsync();
            //    }
            //    else
            //    {
            //        // ❌ Ошибка сервера: откатываем UI
            //        await RollbackAddOperation(itemViewModel, response.Error?.Message ?? "Неизвестная ошибка сервера");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // ❌ Исключение: откатываем UI
            //    await RollbackAddOperation(itemViewModel, $"Исключение: {ex.Message}");
            //}
        }

        private async Task ReloadAllTagsAsync()
        {
            try
            {
                var tags = await _apiClient.GetAsync<List<Tag>>("Tag/AllTags");

                _tagsListingItemViewModels.Clear();

                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        _tagsListingItemViewModels.Add(
                            new TagsListingItemViewModel(tag, _modalNavigationStore, _tagsStore, _apiClient)
                        );
                    }
                }

                Debug.WriteLine($"🔄 Загружено {tags?.Count ?? 0} тегов");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Ошибка перезагрузки тегов: {ex.Message}");
                // Можно показать ошибку пользователю
                ErrorMessage = "Не удалось обновить список тегов";
            }
        }

        private async Task RollbackAddOperation(TagsListingItemViewModel itemViewModel, string errorMessage)
        {
            // 1. Удаляем из UI (откат)
            _tagsListingItemViewModels.Remove(itemViewModel);

            // 2. Показываем ошибку пользователю
            ErrorMessage = $"Не удалось добавить тег: {errorMessage}";

            // 3. Логируем ошибку
            Debug.WriteLine($"❌ Ошибка добавления тега '{itemViewModel.TagName}': {errorMessage}");

            // 4. Можно также показать модальное окно с ошибкой (опционально)
            // _modalNavigationStore.CurrentViewModel = new ErrorViewModel(errorMessage);

            await Task.CompletedTask; // Для соблюдения async сигнатуры
        }
        protected override void Dispose()
        {
            _tagsStore.TagAdded -= OnTagAdded;
            _tagsStore.TagUpdated -= TagsStore_TagUpdated;
            _tagsStore.TagDeleted -= TagsStore_TagDeleted;

            base.Dispose();
        }
        private async void TagsStore_TagUpdated(Tag tag)
        {
            TagsListingItemViewModel? tagViewModel =
                _tagsListingItemViewModels.FirstOrDefault(x => x.Tag.Id == tag.Id);
            var oldTag = tagViewModel.Tag;

            if (tagViewModel != null)
            {
                tagViewModel.Update(tag);
            }
            try
            {
                var resp = await _myBlog.PostAsJsonAsync("https://localhost:7007/Tag/Update", tag);
                //var createdTag = await resp.Content.ReadFromJsonAsync<Tag>();
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Ошибка обновления тега: {tagViewModel.TagName}" +
                        Environment.NewLine + $"Код ошибки: {resp.StatusCode}");
                    //Откат в UI
                    tagViewModel.Update(oldTag);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }
        private async void TagsStore_TagDeleted(int id)
        {
            TagsListingItemViewModel? tagViewModel =
                _tagsListingItemViewModels.FirstOrDefault(x => x.Tag.Id == id);
            if (tagViewModel != null)
            {
                _tagsListingItemViewModels.Remove(tagViewModel);
            }
        }
        public async void LoadTagsAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;

                const string endpoint = "Tag/AllTags";

                var tags = await _apiClient.GetAsync<List<Tag>>(endpoint);

                if (tags != null)
                {
                    //Tags.Clear();
                    _tagsListingItemViewModels.Clear();
                    foreach (var tag in tags)
                    {
                        //Tags.Add(tag);
                        _tagsListingItemViewModels.Add(
                            new TagsListingItemViewModel(tag, _modalNavigationStore, _tagsStore, _apiClient));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки данных: {ex.Message}");
            }
            finally 
            { 
                IsLoading = false; 
            }
        }
    }
}
