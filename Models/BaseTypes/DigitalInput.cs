using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models.Deprecated.BaseTypes
{
    /// <summary>
    /// Construction Data for a digital input
    /// </summary>
    public class DigitalInput : BindableBase, INamedClass, IIDNumber
    {
        /// <summary>
        /// The name for this particular digital input
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// The input id of the digital device
        /// </summary>
        public uint ID
        {
            get { return _iD; }
            set { SetProperty(ref _iD, value); }
        }

        #region Private Fields
        private string _name = "OzDigitalInputData";
        private uint _iD;
        #endregion
    }
}
