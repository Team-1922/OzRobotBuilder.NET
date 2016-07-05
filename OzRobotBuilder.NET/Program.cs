using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib.Model;

namespace OzRobotBuilder.NET
{
    static class Program
    {
        static RobotBuilderApplication robotApp;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            robotApp = new RobotBuilderApplication();
            robotApp.Run(null);
        }
    }
}
