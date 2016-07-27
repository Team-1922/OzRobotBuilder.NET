using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using Team1922.Contracts;

namespace Team1922.MVVM.Models
{
    /// <summary>
    /// The means the global io can be accessed
    /// </summary>
    public static class IOService
    {
        /// <summary>
        /// The global IO Service instance
        /// </summary>
        [Import("Robot.IRobotIOService")]
        public static IRobotIOService Instance { get; }
    }
}
