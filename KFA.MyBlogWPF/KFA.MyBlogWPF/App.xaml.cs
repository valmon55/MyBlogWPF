using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Articles;
using KFA.MyBlogWPF.ViewModels.Roles;
using KFA.MyBlogWPF.ViewModels.Tags;
using KFA.MyBlogWPF.ViewModels.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace KFA.MyBlogWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            App.AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<HttpClient>();
                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<TagsStore>();
                    services.AddSingleton<RolesStore>();
                    services.AddSingleton<UsersStore>();
                    services.AddSingleton<SelectedUserStore>();
                    services.AddSingleton<ArticleStore>();
                    services.AddSingleton<SelectedArticleStore>();

                    services.AddTransient<ArticlesViewModel>();
                    services.AddTransient<TagsViewModel>();
                    services.AddTransient<RolesViewModel>();
                    services.AddTransient<UsersViewModel>();
                    services.AddTransient<LoginViewModel>();
                    services.AddTransient<RegisterViewModel>();
                    services.AddTransient<LogoutViewModel>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            try 
            {
                await AppHost.StartAsync();
                var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();

                mainWindow.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка при запуске приложения: {ex.Message}\n\n{ex.StackTrace}",
                "Критическая ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

                Current.Shutdown();
            }
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            AppHost.Dispose();
            base.OnExit(e);
        }
    }
}
