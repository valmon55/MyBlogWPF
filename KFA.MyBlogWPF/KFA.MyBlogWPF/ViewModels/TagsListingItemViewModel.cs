using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    public class TagsListingItemViewModel : ViewModelBase
    {
        public Tag Tag { get; private set; }
        public string TagName => Tag.Name;
        private bool isDeleting;
        public bool IsDeleting
        {
            get
            {
                return isDeleting;
            }
            set
            {
                isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return ErrorMessage;
            }
            set 
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public TagsListingItemViewModel(Tag tag, ModalNavigationStore modalNavigationStore, TagsStore tagsStore)
        {
            Tag = tag;
            
            EditCommand = new OpenEditTagCommand(this, modalNavigationStore, tagsStore);
            DeleteCommand = new DeleteTagCommand(this, tagsStore);
        }

        public void Update(Tag tag)
        {
            Tag = tag;

            OnPropertyChanged(nameof(TagName));
        }
    }
}
