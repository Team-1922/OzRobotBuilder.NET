using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// This does not add anything new
    /// </summary>
    public interface IRobotMapProvider : IProvider
    {
        /// <summary>
        /// Sets the robot model instance for this robot map
        /// </summary>
        /// <param name="robot">the main robot instance</param>
        void SetRobot(Robot robot);
    }
}
