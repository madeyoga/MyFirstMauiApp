namespace MauiApp1.Windows;

public class SettingsWindow : Window
{
    public SettingsWindow() : base()
    {
        Width = 450;
        Height = 450;
        Title = "Settings";
    }

    public SettingsWindow(Page page) : base(page)
    {
        Width = 450;
        Height = 450;
        Title = "Settings";
        Page = page;
    }
}
