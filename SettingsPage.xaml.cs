using MauiApp1.Models;

namespace MauiApp1;

public partial class SettingsPage : ContentPage
{
    private readonly AppSettings settings;

    public SettingsPage(AppSettings settings)
	{
		InitializeComponent();
        this.settings = settings;

        MainCommandEntry.Text = settings.MainCommand;
        MainWorkingDirEntry.Text = settings.MainWorkingDirectory;
        UpdateCommandEntry.Text = settings.UpdateCommand;
        UpdateWorkingDirEntry.Text = settings.UpdateWorkingDirectory;
    }

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        settings.MainCommand = MainCommandEntry.Text;
        settings.MainWorkingDirectory = MainWorkingDirEntry.Text;
        settings.UpdateCommand = UpdateCommandEntry.Text;
        settings.UpdateWorkingDirectory = UpdateWorkingDirEntry.Text;
        settings.Save();
    }
}