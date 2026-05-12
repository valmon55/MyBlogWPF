using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    public class TagDetailsFormViewModel : ViewModelBase
    {
        private string tagName;
        public string TagName
        {
            get { return tagName; }
            set
            {
                tagName = value;
                OnPropertyChanged(nameof(TagName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        public bool CanSubmit => !string.IsNullOrEmpty(TagName);
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public TagDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
