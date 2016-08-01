using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IPIDControllerSRXProvider
    {
        double P { get; set; }
        double I { get; set; }
        double D { get; set; }
        double F { get; set; }
        double IZone { get; set; }
        double ClosedLoopRampRate { get; set; }
        int AllowableCloseLoopError { get; set; }
        CANTalonDifferentiationLevel SourceType { get; set; }

        void SetPIDController(PIDControllerSRX pidController);
    }
}
