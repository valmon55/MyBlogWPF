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
        private readonly ModalNavigationStore _modalNavigationStore;
        public TagDetailsFormViewModel TagDetailsFormViewModel { get; }
        public AddTagViewModel(ModalNavigationStore modalNavigationStore) 
        {
            _modalNavigationStore = modalNavigationStore;
            ICommand cancelCommand = new CloseModalCommand(_modalNavigationStore);
            TagDetailsFormViewModel = new TagDetailsFormViewModel(null, cancelCommand);
        }
    }
}
