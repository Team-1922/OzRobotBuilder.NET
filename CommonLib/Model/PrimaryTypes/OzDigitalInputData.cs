using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /// <summary>
    /// Construction Data for a digital input
    /// </summary>
    public class OzDigitalInputData : INamedClass, IIdentificationNumber
    {
        /// <summary>
        /// The name for this particular digital input
        /// </summary>
        public string Name { get; set; } = "OzDigitalInputData";
        /// <summary>
        /// The input id of the digital device
        /// </summary>
        public uint ID { get; set; }
    }
}
