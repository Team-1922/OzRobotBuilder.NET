using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
	/*
	 * 
	 * 
	 * 
	 */
    class OzQuadEncoderData
    {
		//while having a 'UnitsPerRotation' value is nice, this piece of data is usually up to emprical testing, therefore
		//		a second attribute for 'UnitsPerRotation' would be pointless
        public double ConversionRatio;

		//This value could exist, but in the context encoders are usually used, it would be pointless
        //public double InitialOffset;

		//the digital in location of the first three encoder wires (this is -1 on TalonSRX)
        public int DigitalIn0;

		//the digital in location of the final encoder wire (this is -1 on TalonSRX)
        public int DigitalIn1;
    }
}
