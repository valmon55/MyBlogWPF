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
        public string TagName { get; }
        public ICommand EditTag { get; }
        public ICommand DeleteTag { get; }
        public TagsListingItemViewModel(string tagName)
        {
            TagName = tagName;
        }
    }
}
