using MauiApp1.Models;
using System.Diagnostics;

namespace MauiApp1.Services;
public class UpdateService : ProcessServiceBase
{
    private readonly AppSettings settings;

    public UpdateService(AppSettings settings)
    {
        this.settings = settings;
    }

    public override void CreateProcess()
    {
        MainProcess = new Process();
        MainProcess.StartInfo.WorkingDirectory = settings.UpdateWorkingDirectory;

        string[] fullcommand = settings.UpdateCommand.Split(" ", 2);
        string filename = fullcommand.First();
        string command = fullcommand.Last();

        MainProcess.StartInfo.FileName = filename;
        MainProcess.StartInfo.Arguments = command;
        MainProcess.StartInfo.UseShellExecute = false;
        MainProcess.StartInfo.CreateNoWindow = true;
        MainProcess.StartInfo.RedirectStandardOutput = true;
        MainProcess.StartInfo.RedirectStandardError = true;
        MainProcess.StartInfo.RedirectStandardInput = true;
    }
}
