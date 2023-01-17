using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.Windows;
using System.Diagnostics;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	private readonly ProcessService processService;
    private readonly UpdateService updateService;
    private readonly AppSettings settings;

    public MainPage(ProcessService processService, UpdateService updateService, AppSettings settings)
	{
		InitializeComponent();
		this.processService = processService;
        this.updateService = updateService;
        this.settings = settings;
    }

	private async void OnStartClicked(object sender, EventArgs e)
    {
		await Task.Yield();

        if (StartBtn.Text == "Stop")
        {
            StartBtn.Text = "Start";
            StartBtn.IsEnabled = false;

            processService.Stop();
            UpdateBtn.IsEnabled = true;

            AppStatusLabel.Text = "Off";
            AppStatusLabel.TextColor = Colors.Red;

			AppPidLabel.IsVisible = false;
        }
		else
        {
            StartBtn.Text = "Stop";
            StartBtn.IsEnabled = false;

            AppendEditor("Starting the application...");
            UpdateBtn.IsEnabled = false;

            processService.CreateProcess();
            processService.MainProcess.OutputDataReceived += new DataReceivedEventHandler(OnOutputDataReceived);
            processService.MainProcess.ErrorDataReceived += new DataReceivedEventHandler(OnOutputDataReceived);
            processService.Start();

			AppStatusLabel.Text = "Running";
			AppStatusLabel.TextColor = Colors.Green;

            AppPidLabel.IsVisible = true;
            AppPidLabel.Text = processService.MainProcess.Id.ToString();
        }

        await Task.Delay(1500);

		StartBtn.IsEnabled = true;
		SemanticScreenReader.Announce(AppStatusLabel.Text);
    }

    private async void OnUpdateClicked(object sender, EventArgs e)
    {
        await Task.Yield();

        UpdateBtn.IsEnabled = false;
        StartBtn.IsEnabled = false;

        if (ProcessHelper.IsRunning(processService.MainProcess))
        {
            await DisplayAlert("Error", "Cannot update while the program is running.", "Cancel");
        }
        else
        {
            AppendEditor("Updating the application...");
            updateService.CreateProcess();
            updateService.MainProcess.OutputDataReceived += new DataReceivedEventHandler(OnOutputDataReceived);
            updateService.MainProcess.ErrorDataReceived += new DataReceivedEventHandler(OnOutputDataReceived);
            updateService.Start();
        }
        
        await updateService.MainProcess.WaitForExitAsync();

        UpdateBtn.IsEnabled = true;
        StartBtn.IsEnabled = true;
    }

    private void OnSettingsClicked(object sender, EventArgs e)
    {
        Window settingsWindow = new SettingsWindow(new SettingsPage(settings));
        
        Application.Current.OpenWindow(settingsWindow);
    }

    private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
    {
		AppendEditor(e.Data);
    }

	private void AppendEditor(string text)
    {
        if (MainThread.IsMainThread)
        {
            string newText = $"{LogsEditor.Text}\n{text}";
            LogsEditor.UpdateText(newText);
            LogsEditor.CursorPosition = newText.Length;
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                string newText = $"{LogsEditor.Text}\n{text}";
                LogsEditor.UpdateText(newText);
				LogsEditor.CursorPosition = newText.Length;
            });
        }
    }
}

