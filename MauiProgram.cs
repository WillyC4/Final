using Final.Repositorio;
using Microsoft.Extensions.Logging;

namespace Final
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
                    fonts.AddFont("KronaOne-Regular.ttf", "KronaOne");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            string dbPath = FileAccessHelper.GetLocalFilePath("AutoPerfectoSQL.db3");
            builder.Services.AddSingleton<SQLiteRepo>(s => ActivatorUtilities.CreateInstance<SQLiteRepo>(s, dbPath));

            return builder.Build();
        }
    }
}
