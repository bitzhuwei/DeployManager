using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.IO;
using System.Diagnostics;
using System.Threading;
//using Log;
//using System.Windows.Forms;

namespace DeployLib
{
    /// <summary>
    /// WMI基本封装
    /// </summary>
    public class WmiShareFunction
    {
        /// <summary>
        /// 用net use命令连接到远程共享目录上，创建网络共享连接
        /// </summary>
        /// <param name="Server">目标ip</param>
        /// <param name="ShareName">远程共享名</param>
        /// <param name="Username">远程登录用户</param>
        /// <param name="Password">远程登录密码</param>
        public static void CreateShareNetConnect(string Server, string ShareName, string Username, string Password)
        {
            Process process = new Process();
            process.StartInfo.FileName = "net.exe";
            process.StartInfo.Arguments = 
                string.Format(@"use \\{1}\{2}\ {0}{3}{0} /user:{0}{4}{0} ", "\"", Server, ShareName, Password, Username);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            process.Close();
            //process.Dispose();
        }

        /// <summary>
        /// 用net use delete命令移除网络共享连接
        /// </summary>
        /// <param name="Server">目标ip</param>
        /// <param name="ShareName">远程共享名</param>
        /// <param name="Username">远程登录用户</param>
        /// <param name="Password">远程登录密码</param>
        public static void RemoveShareNetConnect(string Server, string ShareName, string Username, string Password)
        {
            Process process = new Process();
            process.StartInfo.FileName = "net.exe";
            process.StartInfo.Arguments = 
                string.Format(@"use \\{0}\{1}\ /delete /y", Server, ShareName);
                //string.Format(@"use \\{1}\{2}\ /delete /y", "\"", Server, ShareName);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            process.Close();
            //process.Dispose();

        }
    }
}