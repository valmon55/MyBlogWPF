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
        private readonly ITagService _tagService;

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
            TagsStore tagsStore,
            ITagService tagService)
        {
            _apiClient = apiClient;
            _apiSettings = apiSettings;
            _appSettings = appSettings;
            _featureFlags = featureFlags;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
            _tagService = tagService;

            ApplicationName = _appSettings.ApplicationName;

            // Проверяем фичу-флаг
            if (_featureFlags.EnableExperimentalFeatures)
            {
                // Показываем экспериментальный UI
            }

            _tagsListingItemViewModels = new ObservableCollection<TagsListingItemViewModel>();
            Tags = new ObservableCollection<Tag>();

            // 🔍 ЛОГ: проверяем, сколько раз подписываемся
            // 🔍 ЛОГ: проверяем количество подписчиков ДО подписки
            Debug.WriteLine($"📌 ДО подписки: количество подписчиков = {_tagsStore.GetTagAddedSubscriberCount()}");
            Debug.WriteLine($"📌 ДО подписки: {_tagsStore.GetTagAddedSubscribersInfo()}");


            //_tagsStore.TagAdded += TagsStore_TagAdded;
            _tagsStore.TagAdded += OnTagAdded;
            _tagsStore.TagUpdated += OnTagUpdated;
            _tagsStore.TagUpdated += OnTagUpdated;
            //_tagsStore.TagDeleted += TagsStore_TagDeleted;
            _tagsStore.TagDeleted += OnTagDeleted;

            ReloadAllTagsAsync();
        }

        private void OnTagDeleted(int id)
        {
            TagsListingItemViewModel? tagViewModel =
                _tagsListingItemViewModels.FirstOrDefault(x => x.Tag.Id == id);
            if (tagViewModel != null)
            {
                _tagsListingItemViewModels.Remove(tagViewModel);
            }
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
                _apiClient,
                _tagService
                );
            _tagsListingItemViewModels.Add(itemViewModel);
            await ReloadAllTagsAsync();
        }

        private async Task ReloadAllTagsAsync()
        {
            try
            {
                var tags = await _tagService.GetAllTagAsync();

                _tagsListingItemViewModels.Clear();

                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        _tagsListingItemViewModels.Add(
                            new TagsListingItemViewModel(tag, _modalNavigationStore, _tagsStore, _apiClient, _tagService)
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
        protected override void Dispose()
        {
            _tagsStore.TagAdded -= OnTagAdded;
            _tagsStore.TagUpdated -= OnTagUpdated;
            _tagsStore.TagDeleted -= OnTagDeleted;

            base.Dispose();
        }
        private async void OnTagUpdated(Tag tag)
        {
            foreach(var itemViewModel in _tagsListingItemViewModels)
            {
                if (itemViewModel.Tag.Id == tag.Id)
                {
                    itemViewModel.Tag.Name = tag.Name;
                    break;
                }
            }
            await ReloadAllTagsAsync();
        }
        //public async void LoadTagsAsync()
        //{
        //    try
        //    {
        //        IsLoading = true;
        //        ErrorMessage = null;

        //        const string endpoint = "Tag/AllTags";

        //        var tags = await _apiClient.GetAsync<List<Tag>>(endpoint);

        //        if (tags != null)
        //        {
        //            //Tags.Clear();
        //            _tagsListingItemViewModels.Clear();
        //            foreach (var tag in tags)
        //            {
        //                //Tags.Add(tag);
        //                _tagsListingItemViewModels.Add(
        //                    new TagsListingItemViewModel(tag, _modalNavigationStore, _tagsStore, _apiClient, _tagService));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Ошибка загрузки данных: {ex.Message}");
        //    }
        //    finally 
        //    { 
        //        IsLoading = false; 
        //    }
        //}
    }
}
