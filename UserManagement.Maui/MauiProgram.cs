using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using UserManagement.Maui.Services;
using UserManagement.Maui.ViewModels;

namespace UserManagement.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddTransient<HttpClient>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<IUserApiService, UserApiService>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
