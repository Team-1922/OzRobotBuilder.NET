using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotModel.ResourceManager.PrimaryTypes
{
    /*
     * 
     * All potentiometers will return a value from 0 to 1 (normalized potentiometer units) no matter what
     * 
     */
    class OzPotentiometerData
    {
        //the ratio defined as output units per normalized potentiometer unit
        public double ConversionRatio;

        //the sensor offset (in normalized potentiometer units); the new "zero point" of the sensor
        public double OffsetValue;

        //the Analog In which this potentiometer is plugged into (this is always -1 if plugged into TalonSRX)
        public int AnalogInID;
    }
}
