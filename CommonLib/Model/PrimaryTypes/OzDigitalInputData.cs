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
    public class OzDigitalInputData : INamedClass
    {
        /// <summary>
        /// The name for this particular digital input
        /// </summary>
        public string Name = "OzDigitalInputData";
        /// <summary>
        /// The input id of the digital device
        /// </summary>
        public uint DigitalInputId;
        #region INamedClass Interface
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string GetName()
        {
            return Name;
        }
        public void SetName(string name)
        {
            Name = name;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion
    }
}
