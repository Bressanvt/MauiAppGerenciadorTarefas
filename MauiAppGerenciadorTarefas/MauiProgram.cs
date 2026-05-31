using MauiAppGerenciadorTarefas.Services;
using MauiAppGerenciadorTarefas.ViewModels;
using MauiAppGerenciadorTarefas.Views;

namespace MauiAppGerenciadorTarefas;

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

        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddTransient<ListaTarefasViewModel>();
        builder.Services.AddTransient<NovaTarefaViewModel>();
        builder.Services.AddTransient<ListaTarefasPage>();
        builder.Services.AddTransient<NovaTarefaPage>();

        return builder.Build();
    }
}
