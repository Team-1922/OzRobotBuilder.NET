using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models.BaseTypes
{
    /// <summary>
    /// The data for a quadrature encoder
    /// </summary>
    public class QuadEncoder : BindableBase, INamedClass, IIDNumber
    {
        /// <summary>
        /// The name of this encoder
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// The ratio defined as output units per encoder unit
        /// </summary>
        public double ConversionRatio
        {
            get { return _conversionRatio; }
            set { SetProperty(ref _conversionRatio, value); }
        }
        /// <summary>
        /// The first digital input used (where the three wires are plugged into)
        /// </summary>
        public uint ID
        {
            get { return _iD; }
            set { SetProperty(ref _iD, value); }
        }
        /// <summary>
        /// The second digital input used (where the one wire is plugged into)
        /// </summary>
        public uint ID1
        {
            get { return _iD1; }
            set { SetProperty(ref _iD1, value); }
        }

        #region Private Fields
        private string _name = "OzQuadEncoderData";
        private double _conversionRatio = 1;
        private uint _iD;
        private uint _iD1;
        #endregion
    }
}
