using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Framework;
using ExpenseTracker.Server.Data;
using ExpenseTracker.Server.DataServices.Extensions;
using ExpenseTracker.MauiNativeApp.Views;
using ExpenseTracker.MauiNativeApp.Features.Products;
using ExpenseTracker.MauiNativeApp.ViewModels;
using ExpenseTracker.MauiNativeApp.Services;
using CommunityToolkit.Maui;

namespace ExpenseTracker.MauiNativeApp;

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

        // Configure SQLite Database
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "expensetracker.db");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Register Local Data Services (direct database access)
        builder.Services.AddServerSideFeatureServices();

        // Register Framework Services
        builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

        // Register ViewModels
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<ProductListPageViewModel>();
        builder.Services.AddTransient<ProductFormPageViewModel>();

        // Register Views
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<Views.Products.ProductListPage>();
        builder.Services.AddTransient<Views.Products.ProductFormPage>();

#if DEBUG
        builder.Services.AddLogging(logging =>
        {
            logging.AddDebug();
        });
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Ensure database is created
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();
        }

        return app;
    }
}
