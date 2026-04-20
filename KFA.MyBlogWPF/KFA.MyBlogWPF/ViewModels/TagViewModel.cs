using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KFA.MyBlogWPF.ViewModels
{
    public class TagViewModel : INotifyPropertyChanged
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
        public TagViewModel(HttpClient myBlog)
        {
            _myBlog = myBlog;
            Tags = new ObservableCollection<Tag>();
            LoadTagsAsync();
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
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        } 

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
