using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Comments
{
    public class CommentDetailsFormViewModel : ViewModelBase
    {
        private string commentText;
        public string CommentText
        {
            get { return commentText; }
            set
            {
                commentText = value;
                OnPropertyChanged(nameof(CommentText));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        public bool CanSubmit => !string.IsNullOrEmpty(CommentText);
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public CommentDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
