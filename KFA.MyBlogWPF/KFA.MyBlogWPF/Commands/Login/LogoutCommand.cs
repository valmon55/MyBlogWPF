using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KFA.MyBlogWPF.Commands.Login
{
    public class LogoutCommand : AsyncCommandBase
    {
        private readonly LogoutViewModel _logoutViewModel;
        private readonly IAuthService _authService;
        private readonly ModalNavigationStore _modalNavigationStore;
        private bool isExecuting = false;

        public LogoutCommand(LogoutViewModel logoutViewModel, 
            IAuthService authService, 
            ModalNavigationStore modalNavigationStore)
        {
            _logoutViewModel = logoutViewModel;
            _authService = authService;
            _modalNavigationStore = modalNavigationStore;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (isExecuting)
            {
                Debug.WriteLine("⚠️ LogoutCommand уже выполняется");
                return;
            }
            try
            {
                isExecuting = true;
                _logoutViewModel.IsLoading = true;
                _logoutViewModel.ErrorMessage = null;

                Debug.WriteLine("🔓 Выполняется выход из системы...");

                // 1. Спрашиваем подтверждение (опционально)
                var confirmResult = MessageBox.Show(
                    "Вы уверены, что хотите выйти?",
                    "Подтверждение выхода",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirmResult != MessageBoxResult.Yes)
                {
                    Debug.WriteLine("⏹️ Выход отменен пользователем");
                    return;
                }

                // 2. Отправляем запрос на сервер (если есть)
                await _authService.LogoutAsync();
                SessionStateMessenger.SendSessionStateChanged(SessionState.Login);

                Debug.WriteLine("✅ Выход из системы выполнен успешно");
                _modalNavigationStore.Close();
            }
            catch (Exception ex)
            {
                // Даже если сервер вернул ошибку, мы всё равно очищаем локальное состояние
                _logoutViewModel.ErrorMessage = $"Ошибка при выходе: {ex.Message}";
                Debug.WriteLine($"❌ Ошибка при выходе: {ex.Message}");

                // Всё равно выполняем локальный выход
                MessageBox.Show(
                    "Не удалось связаться с сервером, но локальный выход выполнен.",
                    "Выход выполнен частично",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            finally
            {
                _logoutViewModel.IsLoading = false;
                isExecuting = false;
            }
        }
        //public override bool CanExecute(object? parameter)
        //{
        //    return isExecuting &&
        //           !_logoutViewModel.IsLoading;
        //}
    }
}
