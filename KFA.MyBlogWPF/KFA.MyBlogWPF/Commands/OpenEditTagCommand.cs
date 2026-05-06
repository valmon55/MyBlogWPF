using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands
{
    public class OpenEditTagCommand : CommandBase
    {
        private readonly Tag _tag;
        private readonly ModalNavigationStore _modalNavigationStore;
        public OpenEditTagCommand(Tag tag, ModalNavigationStore modalNavigationStore)
        {
            _tag = tag;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            EditTagViewModel editTagViewModel = new EditTagViewModel(_tag, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editTagViewModel;
        }

    }
}
