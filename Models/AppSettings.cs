namespace MauiApp1.Models;

public class AppSettings
{
    public string MainWorkingDirectory { get; set; } = string.Empty;
    public string MainCommand { get; set; } = string.Empty;
    public string UpdateWorkingDirectory { get; set; } = string.Empty; 
    public string UpdateCommand { get; set; } = string.Empty;

    public AppSettings() 
    {
        MainWorkingDirectory = Preferences.Default.Get(AppSettingsKeyDefaults.MainWorkingDirectory, string.Empty);
        MainCommand = Preferences.Default.Get(AppSettingsKeyDefaults.MainCommand, string.Empty);
        UpdateWorkingDirectory = Preferences.Default.Get(AppSettingsKeyDefaults.UpdateWorkingDirectory, string.Empty);
        UpdateCommand = Preferences.Default.Get(AppSettingsKeyDefaults.UpdateCommand, string.Empty);
    }

    public void Save()
    {
        Preferences.Set(AppSettingsKeyDefaults.MainWorkingDirectory, MainWorkingDirectory);
        Preferences.Set(AppSettingsKeyDefaults.MainCommand, MainCommand);
        Preferences.Set(AppSettingsKeyDefaults.UpdateWorkingDirectory, UpdateWorkingDirectory);
        Preferences.Set(AppSettingsKeyDefaults.UpdateCommand, UpdateCommand);
    }
}

public static class AppSettingsKeyDefaults
{
    public const string MainWorkingDirectory = "MainWorkingDirectory";
    public const string MainCommand = "MainCommand";
    public const string UpdateWorkingDirectory = "UpdateWorkingDirectory";
    public const string UpdateCommand = "UpdateCommand";
}