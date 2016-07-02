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
        public static SDIDocumentManager DocManager = new FolderDocumentManager();
        public static KeyValueController Controller = new KeyValueController();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CommonLib.Model.Serializers.BuiltInLoaders.RegisterLoaders(DocManager);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Views.MainView());
        }
    }
}
