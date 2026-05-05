using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    public class EditTagViewModel : ViewModelBase
    {
        public TagDetailsFormViewModel TagsDetailFormViewModel { get; }
        public EditTagViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            TagsDetailFormViewModel = new TagDetailsFormViewModel(null, cancelCommand);
        }
    }
}
