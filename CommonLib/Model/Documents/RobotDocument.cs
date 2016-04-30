using CommonLib.Interfaces;
using CommonLib.Model.Compositetypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.Documents
{
    public class RobotDocument : Document
    {
        public List<OzSubsystemData> Subsystems = new List<OzSubsystemData>();
        //List<OzCommandData> Commands = new List<OzCommandData>();
    }
}
