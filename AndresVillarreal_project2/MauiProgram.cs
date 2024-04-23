using AndresVillarreal_project2.Views;
using AndresVillarreal_project2.ViewModels;
using AndresVillarreal_project2.Data;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace AndresVillarreal_project2
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

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<CityRepository>();
            builder.Services.AddSingleton<ProvinceRepository>();

            builder.Services.AddSingleton<CityDetailsPage>();
            builder.Services.AddSingleton<CityDetailsViewModel>();


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
