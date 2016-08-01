using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    public interface IAnalogInputProvider
    {
        int ID { get; set; }
        string Name { get; set; }
        int AccumulatorCenter { get; set; }
        int AccumulatorDeadBand { get; set; }
        int AccumulatorInitialValue { get; set; }
        int AverageBitsField { get; set; }
        int OversampleBits { get; set; }
        double ConversionRatio { get; set; }
        double SensorOffset { get; set; }
        int RawValue { get; set; }
        double Value { get; set; }
        long AccumulatorCount { get; set; }
        long AccumulatorValue { get; set; }

        void SetAnalogInput(AnalogInput analogInput);
    }
}
