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
        public static Views.MainView View;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CommonLib.Model.Serializers.BuiltInLoaders.RegisterLoaders(DocManager);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            View = new Views.MainView();
            Application.Run(View);
        }
    }
}
