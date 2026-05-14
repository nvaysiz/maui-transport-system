using Microsoft.Extensions.Logging;
using Pz2MauiApp.ViewModel;

namespace Pz2MauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //Создавать тольуо один объект WorkViewModel
            builder.Services.AddSingleton<FuelViewModel>();
            builder.Services.AddSingleton<TripViewModel>();
            builder.Services.AddSingleton<VehicleViewModel>();
            builder.Services.AddSingleton<VehiclePage>();
            builder.Services.AddSingleton<FuelPage>();
            builder.Services.AddSingleton<TripPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
