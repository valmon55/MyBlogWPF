using Model = KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class LoginCommand : AsyncCommandBase
    {
        private LoginViewModel _loginViewModel;
        private readonly HttpClient _myBlog;
        public LoginCommand(LoginViewModel loginViewModel, HttpClient myBlog)
        {
            _loginViewModel = loginViewModel;
            _myBlog = myBlog;
        }

        //private readonly LoginViewModel _loginViewModel;

        //public LoginCommand(LoginViewModel loginViewModel)
        //{
        //    _loginViewModel = loginViewModel;
        //}

        public override async Task ExecuteAsync(object parameter)
        {
            /// Отправляем запрос в API
            /// Получаем ответ, если все ок - делаем вилимой часть "SingedIn"
            if (_myBlog is null)
                return;

            //JsonContent content = JsonContent.Create();

            _loginViewModel.ErrorString = string.Empty;

            try
            {
                var resp = await _myBlog.PostAsJsonAsync("https://localhost:7007/User/Login", 
                    new Model.Login() { Email = _loginViewModel.Login, Password = _loginViewModel.Password});
                var result = resp.StatusCode;
                if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
                }
                else
                {
                    _loginViewModel.ErrorString = resp.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
                //MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }

            //if ((string)parameter != "Admin")
            //{
            //    _loginViewModel.ErrorString = "Login is incorrect";
            //}
            //else
            //{
            //    SessionStateMessenger.SendSessionStateChanged(SessionState.Signedin);
            //}
            //return Task.CompletedTask;
        }
    }
}
