using MauiApp1.Models;
using System.Diagnostics;
using System.Management;

namespace MauiApp1.Services;

public class ProcessService
{
    private readonly AppSettings settings;

    public Process MainProcess { set; get; }

    public ProcessService(AppSettings settings)
    {
        this.settings = settings;
    }

    internal void CreateProcess()
    {
        MainProcess = new Process();
        MainProcess.StartInfo.WorkingDirectory = settings.MainWorkingDirectory;

        string[] fullcommand = settings.MainCommand.Split(" ", 2);
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

    internal void Start()
    {
        MainProcess.Start();

        MainProcess.BeginOutputReadLine();
        MainProcess.BeginErrorReadLine();
    }

    internal void Stop()
    {
        if (MainProcess != null)
        {
            MainProcess.CancelOutputRead();
            MainProcess.CancelErrorRead();
            KillProcessAndChildren(MainProcess.Id);
            MainProcess.StandardInput.WriteLine("\x3");
            MainProcess.StandardInput.Close();
            MainProcess.Kill(true);
            MainProcess.CloseMainWindow();
            MainProcess.Dispose();
            MainProcess.Close();
            MainProcess = null;
        }
    }

    private static void KillProcessAndChildren(int pid)
    {
        ManagementObjectSearcher searcher = new("Select * From Win32_Process Where ParentProcessID=" + pid);
        ManagementObjectCollection moc = searcher.Get();
        foreach (ManagementObject mo in moc)
        {
            KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
        }
        try
        {
            Process proc = Process.GetProcessById(pid);
            proc.Kill();
        }
        catch (ArgumentException)
        {
            // Process already exited.
        }
    }
}
