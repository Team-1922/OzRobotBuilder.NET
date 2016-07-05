using CommonLib.Controller;
using CommonLib.Model.Documents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib;
using OzRobotBuilder.NET.Views;

namespace OzRobotBuilder.NET
{
    /// <summary>
    /// The overriden controller for OzRobotBuilder.NET
    /// </summary>
    public class KeyValueController : RobotDocController
    {
        /// <summary>
        /// Constructor to give <see cref="RobotDocController"/> the application reference
        /// </summary>
        /// <param name="app"></param>
        public KeyValueController(CommonLib.Application app) : base(app)
        {
        }

        /// <summary>
        /// Prompts the user to open a file and opens it
        /// </summary>
        public override void OpenFile()
        {
            //pop open dialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                openFileDialog.Filter = "Robot Files (*.robot.json)|*.robot.json";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    App.DocumentManager.OpenDocument(openFileDialog.FileName);
                }

            }

            //finally refresh the grid view and recreate the tree view
            (App.ViewManager.Views["MainView"] as MainView).RecreateTreeView(JsonToDictionaryTree(App.DocumentManager.GetOpenDocJson()));
        }

        public void SaveDocument()
        {
            App.DocumentManager.Save("");
        }
        public void SaveDocumentAs()
        {

        }
    }
}
 