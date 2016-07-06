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
using System.IO;
using CommonLib.Model.CompositeTypes;
using CommonLib.Model.PrimaryTypes;

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
            UpdateViewFromDocument();
        }
        public override void UpdateDocument(string path, string value)
        {
            base.UpdateDocument(path, value);
            UpdateViewFromDocument();
        }
        public void UpdateViewFromDocument()
        {
            var mainView = App.ViewManager.Views["MainView"] as MainView;
            if (null == mainView)
                return;
            mainView.RecreateTreeView(JsonToDictionaryTree(App.DocumentManager.GetOpenDocJson()));
        }

        public void SaveDocument()
        {
            App.DocumentManager.Save("");
        }
        public void SaveDocumentAs()
        {
            //pop the save as dialog
            MessageBox.Show("You have found an unimplemented Feature");
        }
        public void AddSubsystem()
        {
            var doc = App.DocumentManager.OpenDoc as RobotDocument;
            if (null == doc)
                return;
            doc.Subsystems.Add(new OzSubsystemData());
            UpdateViewFromDocument();
        }
        public void AddCommand()
        {
            var doc = App.DocumentManager.OpenDoc as RobotDocument;
            if (null == doc)
                return;
            doc.Commands.Add(new OzCommandData());
            UpdateViewFromDocument();
        }
        public void AddJoystick()
        {
            var doc = App.DocumentManager.OpenDoc as RobotDocument;
            if (null == doc)
                return;
            doc.Joysticks.Add(new OzJoystickData());
            UpdateViewFromDocument();
        }
        public void AddJoystickTrigger()
        {
            var doc = App.DocumentManager.OpenDoc as RobotDocument;
            if (null == doc)
                return;
            doc.Triggers.Add(new OzJoystickTriggerData());
            UpdateViewFromDocument();
        }
        public void AddContinuousTrigger()
        {
            var doc = App.DocumentManager.OpenDoc as RobotDocument;
            if (null == doc)
                return;
            doc.Triggers.Add(new OzContinuousTriggerData());
            UpdateViewFromDocument();
        }
        public void NewDocument()
        {
            MessageBox.Show("You have found an unimplemented Feature");
        }
        public void ImportDocument()
        {
            MessageBox.Show("You have found an unimplemented Feature");
        }
        public void Copy(string selectedNodePath)
        {
            MessageBox.Show("You have found an unimplemented Feature");
        }
        public void Cut(string selectedNodePath)
        {
            MessageBox.Show("You have found an unimplemented Feature");
        }
        public void Paste(string selectedNodePath)
        {
            MessageBox.Show("You have found an unimplemented Feature");
        }
        public void Delete(string selectedNodePath)
        {
            MessageBox.Show("You have found an unimplemented Feature");
        }
        private OzSubsystemData GetSubsystemFromPath(string path)
        {
            var robotDoc = App.DocumentManager.OpenDoc as RobotDocument;
            if (null == robotDoc)
                return null;

            var pathSplit = path.Split(Path.DirectorySeparatorChar);
            if (null == pathSplit)
                return null;
            if (pathSplit.Length < 3)
                return null;
            if (pathSplit[0] != robotDoc.Name)
                return null;
            if (pathSplit[1] != "Subsystems")
                return null;

            //find the correct subsystem
            var name = pathSplit[pathSplit.Length - 1];
            foreach (var subsystem in robotDoc.Subsystems)
            {
                if (subsystem.Name == name)
                    return subsystem;
            }
            return null;
        }
        public void AddPWMMotorController(string selectedNodePath)
        {
            var subsystem = GetSubsystemFromPath(selectedNodePath);
            if (null == subsystem)
                return;
            subsystem.PWMMotorControllers.Add(new OzMotorControllerData());
            UpdateViewFromDocument();
        }
        public void AddTalonSRX(string selectedNodePath)
        {
            var subsystem = GetSubsystemFromPath(selectedNodePath);
            if (null == subsystem)
                return;
            subsystem.TalonSRXs.Add(new OzTalonSRXData());
            UpdateViewFromDocument();
        }
        public void AddAnalogInput(string selectedNodePath)
        {
            var subsystem = GetSubsystemFromPath(selectedNodePath);
            if (null == subsystem)
                return;
            subsystem.AnalogInputDevices.Add(new OzAnalogInputData());
            UpdateViewFromDocument();
        }
        public void AddQuadratureEncoder(string selectedNodePath)
        {
            var subsystem = GetSubsystemFromPath(selectedNodePath);
            if (null == subsystem)
                return;
            subsystem.QuadEncoders.Add(new OzQuadEncoderData());
            UpdateViewFromDocument();
        }
        public void AddDigitalInput(string selectedNodePath)
        {
            var subsystem = GetSubsystemFromPath(selectedNodePath);
            if (null == subsystem)
                return;
            subsystem.DigitalInputs.Add(new OzDigitalInputData());
            UpdateViewFromDocument();
        }
    }
}
 