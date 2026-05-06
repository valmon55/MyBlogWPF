using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    public class TagsListingViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
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
        public TagsListingViewModel(HttpClient myBlog, ModalNavigationStore modalNavigationStore)
        {
            _myBlog = myBlog;
            _tagsListingItemViewModels = new ObservableCollection<TagsListingItemViewModel>();
            Tags = new ObservableCollection<Tag>();
            //LoadTagsAsync();

            AddTag(new Tag() { Name = "C#" }, modalNavigationStore);
            AddTag(new Tag() { Name = "JavaScript" }, modalNavigationStore);
            AddTag(new Tag() { Name = "WPF" }, modalNavigationStore);
            AddTag(new Tag() { Name = "ASP.Net" }, modalNavigationStore);
            AddTag(new Tag() { Name = "Xamarin" }, modalNavigationStore);
            //_tagsListingItemViewModels.Add(new TagsListingItemViewModel("C#"));
            //_tagsListingItemViewModels.Add(new TagsListingItemViewModel("JavaScript"));
            //_tagsListingItemViewModels.Add(new TagsListingItemViewModel("WPF"));
            //_tagsListingItemViewModels.Add(new TagsListingItemViewModel("ASP.Net"));
        }
        private void AddTag(Tag tag, ModalNavigationStore modalNavigationStore)
        {
            ICommand editCommand = new OpenEditTagCommand(tag, modalNavigationStore);
            _tagsListingItemViewModels.Add(new TagsListingItemViewModel(tag, editCommand));
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
                        _tagsListingItemViewModels.Add(new TagsListingItemViewModel(tag, null));
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
