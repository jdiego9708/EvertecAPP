using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SISEvertec.APP.Services.Interfaces;
using SISEvertec.APP.Services;
using SISEvertec.APP.ViewModels.VMContacts;
using SISEvertec.APP.ViewModels.VMHome;
using SISEvertec.APP.ViewModels.VMLogin;
using SISEvertec.APP.ViewModels.VMVisitors;
using SISEvertec.APP.Views.ViewsMessages;
using SISEvertec.APP.Views.ViewsVisitors;
using SISEvertec.Entities.Helpers.Interfaces;
using SISEvertec.Entities.Helpers;
using SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec;
using System.Reflection;
using SISEvertec.APP.Views.ViewsHome;
using SISEvertec.APP.Views.ViewsContacts;
using SISEvertec.APP.Views.ViewsLogin;
using SISEvertec.APP.Views.ViewsClimate;

namespace Evertec.APP;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>().UseMauiCommunityToolkit();

        #region CONFIGURACIÓN DE FUENTES
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        #endregion

        #region INYECCIÓN DE CONFIGURACIÓN
        Assembly GetAssemblyByName(string name)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().
                   SingleOrDefault(assembly => assembly.GetName().Name == name);

            if (assembly == null)
                return null;

            return assembly;
        }

        var a = GetAssemblyByName("SISEvertec.Entities");

        using var stream = a.GetManifestResourceStream("SISEvertec.Entities.Configuration.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(config);
        #endregion

        #region CONFIGURACIÓN SQLITE
        var connectionStringsSections = config.GetSection("ConnectionStrings");
        ConnectionStrings connectionStrings = connectionStringsSections.Get<ConnectionStrings>();

        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EvertecBDFolder");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string databasePath = $"Data Source={Path.Combine(folderPath, connectionStrings.SQLiteConnection)}";

        builder.Services.AddDbContext<EvertecDBContext>(options =>
            options.UseSqlite(databasePath));
        #endregion

        #region INYECCIÓN DE HELPERS AND SERVICES
        builder.Services.AddSingleton<MessagesService>();
        builder.Services.AddSingleton<IRestHelper, RestHelper>();
        builder.Services.AddSingleton<IConfiguration_evertecService, Configuration_evertecService>();
        builder.Services.AddSingleton<IVisitorsService, VisitorsService>();
        #endregion

        #region INYECCIÓN DE VIEW MODELS
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<ContactsViewModel>();
        builder.Services.AddSingleton<VisitorsViewModel>();
        #endregion

        #region INYECCIÓN DE VIEWS
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<TabbedMainPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ContactsPage>();
        builder.Services.AddTransient<VisitorsPage>();
        builder.Services.AddTransient<ClimatePage>();
        #endregion

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
