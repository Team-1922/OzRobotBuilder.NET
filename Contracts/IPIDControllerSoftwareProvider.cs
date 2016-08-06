using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IPIDControllerSoftwareProvider : IProvider
    {
        double P { get; set; }
        double I { get; set; }
        double D { get; set; }
        double F { get; set; }
        double Tolerance { get; set; }
        double CycleDuration { get; set; }
        bool Continuous { get; set; }

        void SetPIDController(PIDControllerSoftware pidController);
    }
}
