using ClientSide.Services.Class;
using ClientSide.Services.InterFace;
using Microsoft.Extensions.Logging;

namespace ClientSide
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
                });
            builder.Services.AddHttpClient("Server", client =>
            {
                client.BaseAddress = new Uri("/");
                client.DefaultRequestHeaders.Add("", "");
            });

            builder.Services.AddScoped<IBarberService, BarberService>();
            builder.Services.AddScoped<IBarberSchedule,BarberSchedule>();
            builder.Services.AddSingleton<PhoneNumberService>();
            builder.Services.AddSingleton<DateTimeService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<LocalizationService>();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<AlertService>();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
