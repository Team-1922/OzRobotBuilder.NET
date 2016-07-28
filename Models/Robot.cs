using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models.Deprecated
{
    /// <summary>
    /// The entire robot application; subsystems, commands, operator interfaces and everything
    /// </summary>
    public class Robot : BindableBase
    {
        /// <summary>
        /// A list of all of the subsystems on this robot
        /// </summary>
        public ObservableCollection<Subsystem> Subsystems { get; set; } = new ObservableCollection<Subsystem>();
        /// <summary>
        /// A list of all of the commands on this robot
        /// </summary>
        public ObservableCollection<Command> Commands { get; set; } = new ObservableCollection<Command>();
        /// <summary>
        /// A list of all of the joysticks configured for this program
        /// </summary>
        public ObservableCollection<Joystick> Joysticks { get; set; } = new ObservableCollection<Joystick>();
    }
}
