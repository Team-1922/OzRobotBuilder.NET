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
        long RawValue { get; }
        double Value { get; }
        double RawVelocity { get; }
        double Velocity { get; }

        void SetQuadEncoder(QuadEncoder quadEncoder);
    }
}
