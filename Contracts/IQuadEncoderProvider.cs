using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IQuadEncoderProvider : IInputProvider
    {
        int ID { get; set; }
        int ID1 { get; set; }
        string Name { get; set; }
        double ConversionRatio { get; set; }
        long RawValue { get; set; }
        double Value { get; set; }
        double RawVelocity { get; set; }
        double Velocity { get; set; }

        void SetQuadEncoder(QuadEncoder quadEncoder);
    }
}
