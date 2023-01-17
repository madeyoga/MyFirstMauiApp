using System.Diagnostics;

namespace MauiApp1.Services;

public abstract class ProcessServiceBase
{
    public Process MainProcess { set; get; }

    public virtual void CreateProcess()
    {
    }

    public void Start()
    {
        MainProcess.Start();

        MainProcess.BeginOutputReadLine();
        MainProcess.BeginErrorReadLine();
    }

    public void Stop()
    {
        if (MainProcess != null)
        {
            MainProcess.CancelOutputRead();
            MainProcess.CancelErrorRead();
            ProcessHelper.KillProcessAndChildren(MainProcess.Id);
            MainProcess.StandardInput.WriteLine("\x3");
            MainProcess.StandardInput.Close();
            MainProcess.Kill(true);
            MainProcess.CloseMainWindow();
            MainProcess.Dispose();
            MainProcess.Close();
            MainProcess = null;
        }
    }
}