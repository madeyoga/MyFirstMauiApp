using MauiApp1.Services;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class App : Application
{
    private readonly ProcessService processService;

    public App(MainPage page, ProcessService processService)
	{
		InitializeComponent();

		MainPage = page;
        this.processService = processService;
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        Window window = base.CreateWindow(activationState);

		const int width = 640;
		const int height = 850;

		window.Width = width;
		window.Height = height;

		window.Title = "My Application";

		window.Destroying += (s, e) =>
		{
			processService.Stop();
		};

		return window;
    }
}
