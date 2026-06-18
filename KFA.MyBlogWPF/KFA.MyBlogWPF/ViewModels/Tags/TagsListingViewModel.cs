using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;
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
        public TagsListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore, TagsStore tagsStore)
        {
            _myBlog = myBlog;
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
            _tagsListingItemViewModels = new ObservableCollection<TagsListingItemViewModel>();
            Tags = new ObservableCollection<Tag>();

            _tagsStore.TagAdded += TagsStore_TagAdded;
            _tagsStore.TagUpdated += TagsStore_TagUpdated;
            _tagsStore.TagDeleted += TagsStore_TagDeleted;

            LoadTagsAsync();

            //AddTag(new Tag() { Name = "C#" }, modalNavigationStore);
            //AddTag(new Tag() { Name = "JavaScript" }, modalNavigationStore);
            //AddTag(new Tag() { Name = "WPF" }, modalNavigationStore);
            //AddTag(new Tag() { Name = "ASP.Net" }, modalNavigationStore);
            //AddTag(new Tag() { Name = "Xamarin" }, modalNavigationStore);
            //_tagsListingItemViewModels.Add(new TagsListingItemViewModel("C#"));
            //_tagsListingItemViewModels.Add(new TagsListingItemViewModel("JavaScript"));
            //_tagsListingItemViewModels.Add(new TagsListingItemViewModel("WPF"));
            //_tagsListingItemViewModels.Add(new TagsListingItemViewModel("ASP.Net"));
        }

        protected override void Dispose()
        {
            _tagsStore.TagAdded -= TagsStore_TagAdded;
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
        private void TagsStore_TagAdded(Tag tag)
        {
            if (_myBlog is null)
                return;
            AddTag(tag);
        }

        private async Task AddTag(Tag tag)
        {
            TagsListingItemViewModel itemViewModel = new TagsListingItemViewModel(tag, _modalNavigationStore, _tagsStore);
            _tagsListingItemViewModels.Add(itemViewModel);
            
            try
            {
                var resp = await _myBlog.PostAsJsonAsync("https://localhost:7007/Tag/AddTag",
                                    new Tag() { Name = itemViewModel.TagName });
                //var createdTag = await resp.Content.ReadFromJsonAsync<Tag>();
                if(!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Ошибка добавления тега: {itemViewModel.TagName}" + 
                        Environment.NewLine + $"Код ошибки: {resp.StatusCode}");
                    //Откат в UI
                    _tagsListingItemViewModels.Remove(itemViewModel);
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
            try
            {
                var resp = await _myBlog.DeleteAsync($"https://localhost:7007/Tag/DeleteTag?id={id}");
                if (!resp.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Ошибка удаления тега: {tagViewModel.TagName}" +
                        Environment.NewLine + $"Код ошибки: {resp.StatusCode}");
                    //Откат в UI
                    _tagsListingItemViewModels.Add(tagViewModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async void LoadTagsAsync()
        {
            if (_myBlog is null)
                return;
            try
            {
                var resp = await _myBlog.GetAsync("https://localhost:7007/Tag/AllTags");
                if (resp.IsSuccessStatusCode)
                {
                    var body = await _myBlog.GetStringAsync("https://localhost:7007/Tag/AllTags");
                    var tags = JsonSerializer.Deserialize<List<Tag>>(body);

                    Tags.Clear();
                    foreach (var tag in tags)
                    {
                        Tags.Add(tag);
                        _tagsListingItemViewModels.Add(new TagsListingItemViewModel(tag, _modalNavigationStore, _tagsStore));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }
    }
}
