using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels
{
    public class AddTagViewModel : ViewModelBase
    {
        public TagDetailsFormViewModel TagsDetailFormViewModel { get; }
        public AddTagViewModel() 
        { 
            TagsDetailFormViewModel = new TagDetailsFormViewModel();
        }
    }
}
