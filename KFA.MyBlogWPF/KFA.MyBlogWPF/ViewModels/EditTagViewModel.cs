using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.ViewModels
{
    public class EditTagViewModel : ViewModelBase
    {
        public TagDetailsFormViewModel TagsDetailFormViewModel { get; }
        public EditTagViewModel()
        {
            TagsDetailFormViewModel = new TagDetailsFormViewModel();
        }
    }
}
