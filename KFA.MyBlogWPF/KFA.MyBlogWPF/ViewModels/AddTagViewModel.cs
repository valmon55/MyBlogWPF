using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Commands.Tag;
using KFA.MyBlogWPF.Services;
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
        public AddTagViewModel(ModalNavigationStore modalNavigationStore, TagsStore tagsStore, IApiClient apiClient)
        {
            ICommand submitCommand = new AddTagCommand(this, modalNavigationStore, tagsStore, apiClient);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            TagDetailsFormViewModel = new TagDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
