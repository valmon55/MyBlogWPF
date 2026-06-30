using KFA.MyBlogWPF.Configuration;
using KFA.MyBlogWPF.Services;
using KFA.MyBlogWPF.Stores;
using KFA.MyBlogWPF.ViewModels;
using KFA.MyBlogWPF.ViewModels.Articles;
using KFA.MyBlogWPF.ViewModels.Comments;
using KFA.MyBlogWPF.ViewModels.Roles;
using KFA.MyBlogWPF.ViewModels.Tags;
using KFA.MyBlogWPF.ViewModels.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    var apiSettings = new ApiSettings();
                    context.Configuration.GetSection("ApiSettings").Bind(apiSettings);

                    if(string.IsNullOrEmpty(apiSettings.BaseURL))
                    {
                        var error = "❌ BaseURL не задан в appsettings.json! Проверьте секцию 'ApiSettings'.";
                        Debug.WriteLine(error);
                        throw new InvalidOperationException(error);
                    }
                    var baseUrl = apiSettings.BaseURL.TrimEnd('/');
                    services.AddSingleton(apiSettings);

                    //Debug.WriteLine($"🔍 Загружен BaseURL: '{apiSettings.BaseURL}'");
                    //Debug.WriteLine($"🔍 Загружен Timeout: {apiSettings.Timeouts.RequestTimeoutSeconds}");

                    var appSettings = new AppSettings();
                    context.Configuration.GetSection("AppSettings").Bind(appSettings);
                    services.AddSingleton(appSettings);

                    var featureFlags = new FeatureFlags();
                    context.Configuration.GetSection("FeatureFlags").Bind(featureFlags);
                    services.AddSingleton(featureFlags);

                    services.AddHttpClient("MyBlogApi", client =>
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        client.Timeout = TimeSpan.FromSeconds(apiSettings.Timeouts.RequestTimeoutSeconds);
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    })
                    .AddPolicyHandler((serviceProvider, request) =>
                    { 
                        return HttpPolicyExtensions
                            .HandleTransientHttpError()
                            .WaitAndRetryAsync(
                                apiSettings.Timeouts.RetryCount,
                                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) ),
                                onRetry: (outcome, timespan, retryAttempt, context) =>
                                { 
                                    Debug.WriteLine($"Retry {retryAttempt} after {timespan.TotalSeconds}s"); 
                                }); 
                    });

                    services.AddSingleton<HttpClient>();
                    // Регистрируем IApiClient вместо HttpClient
                    services.AddSingleton<IApiClient, ApiClient>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<TagsStore>();
                    services.AddSingleton<RolesStore>();
                    services.AddSingleton<UsersStore>();
                    services.AddSingleton<SelectedUserStore>();
                    services.AddSingleton<ArticlesStore>();
                    services.AddSingleton<SelectedArticleStore>();
                    services.AddSingleton<CommentsStore>();

                    services.AddTransient<ArticlesViewModel>();
                    services.AddTransient<CommentsViewModel>();
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
