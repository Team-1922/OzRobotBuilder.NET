using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for command group item viewmodels
    /// </summary>
    public interface ICommandGroupItemProvider : IProvider<CommandGroupItem>
    {
        /// <summary>
        /// The call type of this command group item
        /// </summary>
        CommandGroupMethod CallType { get; set; }
        /// <summary>
        /// The name of the command or command group to call
        /// </summary>
        string Command { get; set; }
        /// <summary>
        /// A comma-separated list of parameters to send to the command
        /// </summary>
        string Parameters { get; set; }
    }
}
