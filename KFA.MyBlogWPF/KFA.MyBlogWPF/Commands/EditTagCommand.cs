using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands
{
    public class EditTagCommand : AsyncCommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditTagCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            // Send API request to Add Tag

            _modalNavigationStore.Close();
        }
    }
}
