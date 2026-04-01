using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using Plugin.Maui.Audio;
using SkiaSharp.Views.Maui.Controls.Hosting;
using StayFitz.Extensions;
using StayFitz.Helper;
using StayFitz.ViewModels;

namespace StayFitz
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //AudioManager
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.RegisterViewModels();
            builder.Services.AddSingleton(LocalizationResourceManager.Instance);
            builder.Services.AddSingleton<HistoryViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
