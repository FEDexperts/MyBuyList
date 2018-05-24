using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class Administration_Controls_MemoryUsage : System.Web.UI.UserControl
{
    protected override void OnLoad(EventArgs e)
    {
        Process process = Process.GetCurrentProcess();
        long memoryKB = ToKiloBytes(process.WorkingSet64);
        long virtualMemoryKB = ToKiloBytes(process.VirtualMemorySize64);
        long peakMemoryKB = ToKiloBytes(process.PeakWorkingSet64);
        
        this.lblMachineName.Text = Environment.MachineName;
        this.lblProcessName.Text = process.ProcessName;
        this.lblProcessStartTime.Text = process.StartTime.ToString("dd/MM/yyyy HH:mm:ss.ffffzzz");
        this.lblMemory.Text = memoryKB.ToString("N0");
        this.lblVirtualMemory.Text = virtualMemoryKB.ToString("N0");
        this.lblPeakMemory.Text = peakMemoryKB.ToString("N0");
        string appPool = Request.ServerVariables["APP_POOL_ID"];
        if (string.IsNullOrEmpty(appPool))
            appPool = "N/A";
        
        this.lblAppPool.Text = appPool;
    }

    private long ToKiloBytes(long bytes)
    {
        return bytes / 1024;
    }
}
