using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model.PrimaryTypes
{
    /*
     * 
     * PID Configuration for software PID Controller (use OzSRXPIDControllerData for SRX PID configuration)
     * 
     */
    public class OzPIDControllerData
    {
        public double P;
        public double I;
        public double D;
        public double F;

        //the time in seconds between update cycles (this is irrelevent on the SRX)
        public double CycleDuration;
        //for use with continuous rotation potentiometers        
        public bool Continuous;
    }
}
