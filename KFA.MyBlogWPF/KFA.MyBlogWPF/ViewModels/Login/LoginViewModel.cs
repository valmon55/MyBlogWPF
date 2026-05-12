using KFA.MyBlogWPF.Commands;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KFA.MyBlogWPF.ViewModels
{
    /// <summary>
    /// Для представления LoginView
    /// Содержит команды Login, Cancel и 
    /// Regster - переход на другую страницу
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private readonly HttpClient _myBlog;
        public ICommand LoginCommand { get; }
        public LoginViewModel(HttpClient myBlog)
        {
            _myBlog = myBlog;

        }
    }
}
