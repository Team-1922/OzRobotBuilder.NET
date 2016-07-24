using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models
{
    /// <summary>
    /// gives a class a name property
    /// </summary>
    public interface INamedClass
    {
        /// <summary>
        /// The name of this instance
        /// </summary>
        string Name { get; set; }
    }
    /// <summary>
    /// gives a class an ID number
    /// </summary>
    public interface IIDNumber
    {
        /// <summary>
        /// The ID of this instance
        /// </summary>
        uint ID { get; set; }
    }
}
