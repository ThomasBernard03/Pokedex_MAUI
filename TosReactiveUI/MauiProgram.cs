using System.ComponentModel.Design;
using TosReactiveUI.Views.Pages;
using TosReactiveUI.ViewModels;
using CsharpTools.Services.Interfaces;
using CsharpTools.Services;
using TosReactiveUI.Services.Interfaces;
using TosReactiveUI.Services;

namespace TosReactiveUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UsePrism(prismAppBuilder => prismAppBuilder
                .RegisterTypes(containerRegistry =>
                {
                    containerRegistry.RegisterServices();
                    containerRegistry.RegisterNavigation();
                    containerRegistry.RegisterHelpers();
                })
                .OnAppStart(navigationService => navigationService.NavigateAsync(nameof(PokemonsPage))))
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}



    private static void RegisterHelpers(this IContainerRegistry containerRegistry)
    {
    }

    private static void RegisterServices(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IHttpService, HttpService>();
        containerRegistry.RegisterSingleton<IPokemonService, JsonFilePokemonService>();
    }

    private static void RegisterNavigation(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<PokemonsPage, PokemonsViewModel>();

        containerRegistry.RegisterForNavigation<NavigationPage>();
    }
}

