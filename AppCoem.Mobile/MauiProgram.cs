using Microsoft.Extensions.Logging;

using AppCoem.Mobile.DataAccess;
using AppCoem.Mobile.ViewModels;
using AppCoem.Mobile.Views;

namespace AppCoem.Mobile
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

            var dbContext = new EAfiliadoDbContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            builder.Services.AddDbContext<EAfiliadoDbContext>();

            builder.Services.AddTransient<EAfiliadoPage>();
            builder.Services.AddTransient<EAfiliadoViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            Routing.RegisterRoute(nameof(EAfiliadoPage), typeof(EAfiliadoPage));


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
