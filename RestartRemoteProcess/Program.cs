using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestartRemoteProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = false; myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.FileName = @".\PSTools\psexec";
            myProcess.StartInfo.Arguments = string.Format(
                @"\\{1} {0}{4}{0} -u {2} -p {3}", "\"",
                "150.111.200.108", "aamis108", "ameco@xinyuan789",
                @"C:\Documents and Settings\aamis108\桌面\EGFS quick launch\restart.bat");//"%SystemRoot%\system32\cmd.exe ");
            //@"\\150.111.200.108 -u ameco\aamis108 -p ameco@xinyuan789 cmd ";
            myProcess.Start(); 
            myProcess.WaitForExit(); 
            string strRst = myProcess.StandardOutput.ReadToEnd();
            Console.WriteLine(strRst);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }

    }
}
