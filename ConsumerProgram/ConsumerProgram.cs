﻿using System;
using System.Collections.Generic;
using System.Linq;
using WPILib;
using WPILib.Commands;
using WPILib.LiveWindow;
using WPILib.SmartDashboard;
using ConsumerProgram.Subsystems;
using ConsumerProgram.Commands;
using ConsumerProgram.ExtLua;
using System.IO;

namespace ConsumerProgram
{
    /// <summary>
    /// The VM is configured to automatically run this class, and to call the
    /// functions corresponding to each mode, as described in the IterativeRobot
    /// documentation. 
    /// </summary>
    public class ConsumerProgram : IterativeRobot
    {
        OzLuaState LuaState = new OzLuaState();
        ResManager.ResManager ResourceManager = new ResManager.ResManager();

        public static OzSubsystem exampleSubsystem;
        public static OI oi;

        Command autonomousCommand;
        SendableChooser chooser;

        // This function is run when the robot is first started up and should be
        // used for any initialization code.
        //
        public override void RobotInit()
        {
            InitLuaState();
            RegisterResourceLoaders();
            LoadResourceCache();
            
            exampleSubsystem = new OzSubsystem(LuaState);

            oi = new OI();
            // instantiate the command used for the autonomous period
            autonomousCommand = new ExampleCommand();
            chooser = new SendableChooser();
            chooser.AddDefault("Default Auto", new ExampleCommand());
            //chooser.AddObject("My Auto", new MyAutoCommand);
            SmartDashboard.PutData("Chooser", chooser);
        }

        #region Initialization Methods
        private bool InitLuaState()
        {
            LuaState.LoadCLRPackage();

            return true;//what can go wrong here?
        }
        private bool RegisterResourceLoaders()
        {
            //possible loaders:
            /*
             
            -XML
            -Plain text
            -JSON


            */

            return true;
        }
        private bool LoadResourceCache()
        {
            try
            {
                ResourceManager.SetCacheLoc("ResCache.zip");
                return true;
            }
            catch(FileNotFoundException e)
            {
                return false;
            }
            return false;
        }
        #endregion 


        public override void DisabledPeriodic()
        {
            Scheduler.Instance.Run();
        }

        // This autonomous (along with the sendable chooser above) shows how to select between
        // different autonomous modes using the dashboard. The senable chooser code works with
        // the Java SmartDashboard. If you prefer the LabVIEW Dashboard, remove all the chooser
        // code an uncomment the GetString code to get the uto name from the text box below
        // the gyro.
        // You can add additional auto modes by adding additional commands to the chooser code
        // above (like the commented example) or additional comparisons to the switch structure
        // below with additional strings and commands.
        public override void AutonomousInit()
        {
            autonomousCommand = (Command)chooser.GetSelected();

            /*
            string autoSelected = SmartDashboard.GetString("Auto Selector", "Default");
            switch(autoSelected)
            {
            case "My Auto":
                autonomousCommand = new MyAutoCommand();
                break;
            case "Default Auto"
            default:
                autonomousCommand = new ExampleCommand();
                break;
            }
            */
            // schedule the autonomous command (example)
            if (autonomousCommand != null) autonomousCommand.Start();
        }


        // This function is called periodically during autonomous
        public override void AutonomousPeriodic()
        {
            Scheduler.Instance.Run();
        }

        public override void TeleopInit()
        {
            // This makes sure that the autonomous stops running when
            // teleop starts running. If you want the autonomous to 
            // continue until interrupted by another command, remove
            // this line or comment it out.
            if (autonomousCommand != null) autonomousCommand.Cancel();
        }

        //
        // This function is called when the disabled button is hit.
        // You can use it to reset subsystems before shutting down.
        //
        public override void DisabledInit()
        {

        }

        //
        // This function is called periodically during operator control
        //
        public override void TeleopPeriodic()
        {
            Scheduler.Instance.Run();
        }

        //
        // This function is called periodically during test mode
        //
        public override void TestPeriodic()
        {
            LiveWindow.Run();
        }
    }
}
