using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections;
using DeployLib;

namespace DeployManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var logFullname = Path.Combine(
                Application.StartupPath, string.Format(
                "DeployManager{0}.log", DateTime.Now.ToString("yyyyMMdd_HHmmss")));
            var listener = new TextWriterTraceListener(logFullname, "log recorder");
            Debug.Listeners.Add(listener);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var message = e.Exception.GetDetailedException();
            Debug.WriteLine(message);
            Debug.Close();
            MessageBox.Show(message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


    }
}
