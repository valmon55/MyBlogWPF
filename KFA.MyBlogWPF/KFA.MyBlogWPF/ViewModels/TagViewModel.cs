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

namespace KFA.MyBlogWPF.ViewModels
{
    public class TagViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _myBlog = new HttpClient();
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
        }

        public TagViewModel()
        {
            var tagList = GetTags().Result;
            foreach(var tag in tagList)
            {
                tags.Add(tag);
            }
        }

        public async Task<List<Tag>> GetTags()
        {
            var tags = new List<Tag>();

            string body;
            var resp = await _myBlog.GetAsync("https://localhost:7007/Tag/AllTags");
            if( resp.IsSuccessStatusCode)
            {
                body = await _myBlog.GetStringAsync("https://localhost:7007/Tag/AllTags");
                tags = JsonSerializer.Deserialize<List<Tag>>(body);
            }
            else
            {
                
            }
            return tags;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
