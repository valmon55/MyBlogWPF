using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    public class RoleDetailsFormViewModel : ViewModelBase
    {
        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set
            {
                roleName = value;
                OnPropertyChanged(nameof(RoleName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string description;
        public string Description 
        { 
            get { return description; } 
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public bool CanSubmit => !string.IsNullOrEmpty(RoleName);
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public RoleDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
