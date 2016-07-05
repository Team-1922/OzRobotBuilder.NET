using CommonLib.Controller;
using CommonLib.Model.Documents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OzRobotBuilder.NET
{
    class KeyValueController : RobotDocController
    {
        /// <summary>
        /// Update the document with the given data path and string value
        /// </summary>
        /// <param name="path">the treeview path to the data</param>
        /// <param name="value">the value to update the data with</param>
        public void UpdateData(string path, string value)
        {
            //good ole null checks
            if (null == path || null == value)
                return;

            var robotDoc = Program.DocManager.OpenDoc as RobotDocument;
            if (null == robotDoc)
                return;

            //generate json text of the currently open document
            var jsonText = Program.DocManager.GetOpenDocJson();
        }
        private void UpdateJsonText(string text, string path, string value)
        {
            var jObject = JObject.Parse(text);
        }
        /// <summary>
        /// Prompts the user to open a file and opens it
        /// </summary>
        public override void OpenFile()
        {

            //pop open dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            openFileDialog.Filter = "Robot Files (*.robot.json)|*.robot.json";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Program.DocManager.OpenDocument(openFileDialog.FileName);
            }


            //finally refresh the grid view and recreate the tree view
            Program.View.RecreateTreeView();
        }
    }
}
 