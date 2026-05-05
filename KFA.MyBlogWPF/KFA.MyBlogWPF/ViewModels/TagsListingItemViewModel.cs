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
        public ICommand EditTagCommand { get; }
        public ICommand DeleteTagCommand { get; }
        public TagsListingItemViewModel(string tagName)
        {
            TagName = tagName;
        }
    }
}
