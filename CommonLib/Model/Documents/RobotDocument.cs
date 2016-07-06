﻿using CommonLib.Interfaces;
using CommonLib.Model.CompositeTypes;
using CommonLib.Model.PrimaryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.Documents
{
    /// <summary>
    /// The entire robot application; subsystems, commands, operator interfaces and everything
    /// </summary>
    public class RobotDocument : Document
    {
        /// <summary>
        /// The name of the robot document; this is always the same becuase only one document can be opened at once
        /// </summary>
        public string Name { get; private set; } = "RobotDocument";
        /// <summary>
        /// A list of all of the subsystems on this robot
        /// </summary>
        public UniqueItemList<OzSubsystemData> Subsystems = new UniqueItemList<OzSubsystemData>();
        /// <summary>
        /// A list of all of the commands on this robot
        /// </summary>
        public UniqueItemList<OzCommandData> Commands = new UniqueItemList<OzCommandData>();
        /// <summary>
        /// A list of all of the joysticks configured for this program
        /// </summary>
        public UniqueItemList<OzJoystickData> Joysticks = new UniqueItemList<OzJoystickData>();
        /// <summary>
        /// A list of all of the triggers configured for this operator control
        /// </summary>
        public UniqueItemList<OzTriggerDataBase> Triggers = new UniqueItemList<OzTriggerDataBase>();
    }
}
