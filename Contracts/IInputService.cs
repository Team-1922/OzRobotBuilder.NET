using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// Represents a single unprocessed unit of robot Input
    /// Child classes will extend with additional hardware features of the different Input types and options
    /// Typically the consumer will back either <see cref="Value"/> or <see cref="ValueAsBool"/>
    /// with access to the Input and just use the other as a conversion
    /// </summary>
    public interface IInputService
    {
        /// <summary>
        /// the value the robot has for this Input
        /// NOTE: sometimes this will return a normalized value, but look at the documentation for each IO service for details
        /// </summary>
        double Value { get; }
        /// <summary>
        /// The boolean value the Input has
        /// </summary>
        bool ValueAsBool { get; }
    }
}
