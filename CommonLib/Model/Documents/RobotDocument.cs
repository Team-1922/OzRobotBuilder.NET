using CommonLib.Interfaces;
using CommonLib.Model.CompositeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.Documents
{
    public class RobotDocument : Document
    {
        public string Name { get; private set; } = "RobotDocument";
        public List<OzSubsystemData> Subsystems = new List<OzSubsystemData>();
        public List<OzCommandData> Commands = new List<OzCommandData>();
    }
}
