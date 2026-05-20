using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels.Users
{
    public class UserDetailsFormViewModel : ViewModelBase
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }
        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        private IEnumerable<Role> roles;
        public IEnumerable<Role> Roles
        {
            get { return roles; }
            set
            {
                roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
        private IEnumerable<RoleAssignment> roleAssignments;
        public IEnumerable<RoleAssignment> RoleAssignments
        {
            get { return roleAssignments; }
            set
            {
                roleAssignments = value;
                OnPropertyChanged(nameof(RoleAssignments));
            }
        }
        private IEnumerable<Role> allRoles;
        public bool CanSubmit => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public UserDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }    

        public void InitRoles(List<Role> allRoles, List<Role> userRoles)
        {
            if(allRoles == null || !allRoles.Any())
            {
                RoleAssignments = new List<RoleAssignment>();
                return;
            }
            this.allRoles = allRoles;

            RoleAssignments = allRoles.Select(role => new RoleAssignment()
            {
                Role = role,
                IsAssignment = userRoles?.Any(ur => ur.Id == role.Id) ?? false
            }).ToList();
        }
        public List<Role> GetSelectedRoles()
        {
            return RoleAssignments?.Where(ra => ra.IsAssignment)
                        .Select(ra => ra.Role).ToList() ?? new List<Role>();
        }
    }
}
