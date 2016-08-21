using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// Represents a single unprocessed unit of robot Output
    /// Child classes will extend with additional hardware features of the different Output types and options
    /// Typically the consumer will back either <see cref="Value"/> or <see cref="ValueAsBool"/>
    /// with access to the output and just use the other as a conversion
    /// </summary>
    public interface IOutputService : IIOService
    {
        /// <summary>
        /// the value the robot has for this Output
        /// NOTE: sometimes this will return a normalized value, but look at the documentation for each IO service for details
        /// </summary>
        double Value { get; set; }
        /// <summary>
        /// The boolean value the output has
        /// </summary>
        bool ValueAsBool { get; set; }
    }
}
