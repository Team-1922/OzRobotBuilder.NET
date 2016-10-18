using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Team1922.MVVM.Models;
using WPILib;
using WPILib.SmartDashboard;

namespace RobotSideApp
{
    /**
 * This is a demo program showing the use of the RobotDrive class.
 * The SampleRobot class is the base of a robot application that will automatically call your
 * Autonomous and OperatorControl methods at the right time as controlled by the switches on
 * the driver station or the field controls.
 *
 * The VM is configured to automatically run this class, and to call the
 * functions corresponding to each mode, as described in the SampleRobot
 * documentation.
 *
 * WARNING: While it may look like a good choice to use for your code if you're inexperienced,
 * don't. Unless you know what you are doing, complex code will be much more difficult under
 * this system. Use IterativeRobot or Command-Based instead if you're new.
 */
    public class RobotSideApp : SampleRobot
    {
        const string defaultAuto = "Default";
        const string customAuto = "My Auto";
        string autoSelected;
        SendableChooser chooser;

        const string robotFile = "Robot.xml";
        RobotSideViewModel _viewModel;

        public RobotSideApp()
        {
        }

        #region SampleRobot
        protected override void RobotInit()
        {
            chooser = new SendableChooser();
            chooser.AddDefault("Default Auto", defaultAuto);
            chooser.AddObject("My Auto", customAuto);
            SmartDashboard.PutData("Chooser", chooser);

            //load the robot file
            XmlSerializer serializer = new XmlSerializer(typeof(Robot));
            Robot robot;
            using (FileStream reader = new FileStream(robotFile, FileMode.Open))
            {
                robot = (Robot)serializer.Deserialize(reader);
            }
            _viewModel = new RobotSideViewModel();
            _viewModel.ModelReference = robot;
        }

        // This autonomous (along with the sendable chooser above) shows how to select between
        // different autonomous modes using the dashboard. The senable chooser code works with
        // the Java SmartDashboard. If you prefer the LabVIEW Dashboard, remove all the chooser
        // code an uncomment the GetString code to get the uto name from the text box below
        // the gyro.
        // You can add additional auto modes by adding additional comparisons to the if-else
        // structure below with additional strings. If using the SendableChooser
        // be sure to add them to the chooser code above as well.
        public override void Autonomous()
        {
            //TODO: support autonomous modes
        }

        /**
         * Runs the RobotViewModelBase.RunBackgroundThread
         */
        public override void OperatorControl()
        {
            while (IsOperatorControl && IsEnabled)
            {
                //loop the viewmodel update thingy
                _viewModel.RunBackgroundThread(false);
            }
        }

        /**
         * Runs during test mode
         */
        public override void Test()
        {
        }
        #endregion

        #region IDisposable
        private bool disposedValue = false; // To detect redundant calls

        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _viewModel?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        protected override void Disabled()
        {
            base.Disabled();
            Dispose(true);
        }
        #endregion
    }
}
