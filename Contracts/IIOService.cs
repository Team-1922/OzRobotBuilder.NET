using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// represents a single unprocessed unit of robot IO
    /// Typically the consumer will back either <see cref="Value"/> or <see cref="ValueAsBool"/>
    ///     with access to the IO and just use the other as a conversion
    /// </summary>
    public interface IIOService
    {
        /// <summary>
        /// the value the robot has for this IO
        /// </summary>
        double Value { get; set; }
        /// <summary>
        /// The boolean value the robot has
        /// </summary>
        bool ValueAsBool { get; set; }
    }
}
