using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployConsole
{
    class Program
    {
        private static DeployLib.DeployCmd cmd = new DeployLib.DeployCmd(false);

        static void Main(string[] args)
        {
            var text = "songqi77";
            var encrypted = DeployLib.Utility.Encrypt(text);
            var decrypted = DeployLib.Utility.Decrypt(encrypted);

            var deployProjectConfig = Path.Combine(Environment.CurrentDirectory, "deploy.xml");
            if (args != null && args.Length > 0)
            {
                deployProjectConfig = args[0];
            }
            
            if (!File.Exists(deployProjectConfig))
            {
                Console.WriteLine(
                    "Please put 'deploy.xml' at the same directory with this app. Or specify your deploy file in cmd-line like 'DeployConsole.exe someDeploy.xml'.");
                Console.WriteLine("Press any key to quit...");
                Console.ReadKey(true);
                return;
            }

            try
            {
                var deployProject = DeployLib.Document.Load(deployProjectConfig);
                var funcAfterProcessedOneEntry = new Func<int, object, bool>(AfterProcessedOneEntry);
                Console.Write("Force deploy every files?('Y' or 'N')");
                while (true)
                {
                    var c = Console.ReadKey(true);
                    if (c.Key == ConsoleKey.Y || c.Key == ConsoleKey.N)
                    {
                        cmd.ForceDeploy = (c.Key == ConsoleKey.Y);
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("Force deploy every files?('Y' or 'N')");
                    }
                }

                Console.CancelKeyPress += Console_CancelKeyPress;

                Console.WriteLine("Deploy starts...");
                var report = deployProject.Project.Deploy(cmd, funcAfterProcessedOneEntry);
                Console.WriteLine("Deploy finished.");
                Console.WriteLine(report);
                Console.WriteLine("Press any key to quit...");
                Console.ReadKey(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            cmd.CancelDeploy = true;
            e.Cancel = true;
        }

        static bool AfterProcessedOneEntry(int percentProgress, object userState)
        {
            Console.WriteLine("Processing:{0}%", percentProgress);
            Console.WriteLine(userState);
            return (!(cmd.CancelDeploy));
        }
    }
}
