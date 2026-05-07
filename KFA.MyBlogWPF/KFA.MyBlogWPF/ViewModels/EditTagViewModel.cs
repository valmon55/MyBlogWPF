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
    public class EditTagViewModel : ViewModelBase
    {
        public int TagId { get; }
        public TagDetailsFormViewModel TagDetailsFormViewModel { get; }
        public EditTagViewModel(Tag tag, TagsStore tagsStore, ModalNavigationStore modalNavigationStore)
        {
            TagId = tag.Id;

            ICommand submitCommand = new EditTagCommand(this, modalNavigationStore, tagsStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            TagDetailsFormViewModel = new TagDetailsFormViewModel(submitCommand, cancelCommand)
            {
                TagName = tag.Name
            };
        }
    }
}
