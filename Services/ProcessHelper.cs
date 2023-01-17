using System.Diagnostics;
using System.Management;

namespace MauiApp1.Services;
internal class ProcessHelper
{
    internal static void KillProcessAndChildren(int pid)
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

    internal static bool IsRunning(Process process)
    {
        if (process == null)
        {
            return false;
        }

        try
        {
            Process.GetProcessById(process.Id);
        }
        catch (ArgumentException)
        {
            return false;
        }
        return true;
    }
}
