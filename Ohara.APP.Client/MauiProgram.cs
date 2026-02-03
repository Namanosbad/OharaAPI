using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ohara.APP.Client.Config;
using Ohara.APP.Client.Services;

namespace Ohara.APP.Client;

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

        builder.Services.AddMauiBlazorWebView();

        builder.Configuration.AddJsonFile(
            "appsettings.json",
            optional: false,
            reloadOnChange: false);

        builder.Services.Configure<APISettings>(
            builder.Configuration.GetSection("APISettings"));


        builder.Services.AddHttpClient("API", (sp, http) =>
        {
            var settings = sp.GetRequiredService<IOptions<APISettings>>().Value;
            http.BaseAddress = new Uri(settings.BaseUrl);
        });

        // 🔗 Services do app
        builder.Services.AddScoped<AutorService>();
        builder.Services.AddScoped<LivroService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}