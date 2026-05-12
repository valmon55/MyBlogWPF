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
    public class AddTagViewModel : ViewModelBase
    {
        public TagDetailsFormViewModel TagDetailsFormViewModel { get; }
        public AddTagViewModel(ModalNavigationStore modalNavigationStore, TagsStore tagsStore)
        {
            ICommand submitCommand = new AddTagCommand(this, modalNavigationStore, tagsStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            TagDetailsFormViewModel = new TagDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
