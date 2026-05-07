using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.Commands
{
    public class OpenAddTagCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly TagsStore _tagsStore;

        public OpenAddTagCommand(ModalNavigationStore modalNavigationStore, TagsStore tagsStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _tagsStore = tagsStore;
        }

        public override void Execute(object? parameter)
        {
            AddTagViewModel addTagViewModel = new AddTagViewModel(_modalNavigationStore, _tagsStore);
            _modalNavigationStore.CurrentViewModel = addTagViewModel;
        }
    }
}
