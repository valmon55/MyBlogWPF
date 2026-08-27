using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Tag;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace KFA.MyBlogWPF.ViewModels.Tags
{
    public class TagsListingItemViewModel : ViewModelBase
    {
        private bool _isNew; // Tag hasn't been sent to server
        public bool IsNew
        {
            get => _isNew;
            set => SetField(ref _isNew, value);
        }
        private Tag _tag;
        public Tag Tag => _tag;
        public string TagName => Tag.Name;
        private bool isDeleting;
        public bool IsDeleting
        {
            get => isDeleting;
            set => SetField(ref isDeleting, value);
                
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetField(ref errorMessage, value);                
        }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public TagsListingItemViewModel(
            Tag tag, 
            ModalNavigationStore modalNavigationStore, 
            TagsStore tagsStore,
            IApiClient apiClient,
            ITagService tagService,
            bool isNew = false)
        {
            _tag = tag;
            _isNew = isNew;

            EditCommand = new OpenEditTagCommand(this, modalNavigationStore, tagsStore);
            DeleteCommand = new DeleteTagCommand(this, tagsStore, apiClient, tagService);
        }

        public void Update(Tag tag)
        {
            _tag = tag;
            _isNew = false;

            OnPropertyChanged(nameof(TagName));
            OnPropertyChanged(nameof(IsNew));
        }
        public void UpdateWithServerData(TagResponse serverData)
        {
            _tag = new Tag() { Id = serverData.Id, Name = serverData.Name };
            _isNew = false;

            OnPropertyChanged(nameof(TagName));
            OnPropertyChanged(nameof(IsNew));
        }
        protected override void Dispose()
        { 
            base.Dispose(); 
        }
    }
}
