using KFA.MyBlogWPF.Models;
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
        public Tag Tag { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public TagsListingItemViewModel(Tag tag, ICommand editCommand)
        {
            Tag = tag;
            EditCommand = editCommand;
        }
    }
}
