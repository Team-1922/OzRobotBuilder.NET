using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace Team1922.MVVM.Models.BaseTypes
{
    /// <summary>
    /// This is more complicated than the potentiometer (and the potentiometer might be merged into this) because of polling rates, accumulation, etc.
    /// </summary>
    public class AnalogInput : BindableBase, INamedClass, IIDNumber
    {
        /// <summary>
        /// the polling rate for ALL analog inputs
        /// TODO: how should the MVVM pattern handle this?
        /// </summary>
        public static double GlobalSampleRate;
        /// <summary>
        /// The name of this particular analog input
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// The input id
        /// </summary>
        public uint ID
        {
            get { return _iD; }
            set { SetProperty(ref _iD, value); }
        }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AccumulatorCenter
        {
            get { return _accumulatorCenter; }
            set { SetProperty(ref _accumulatorCenter, value); }
        }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AccumulatorDeadband
        {
            get { return _accumulatorDeadband; }
            set { SetProperty(ref _accumulatorDeadband, value); }
        }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public long AccumulatorInitialValue
        {
            get { return _accumulatorInitialValue; }
            set { SetProperty(ref _accumulatorInitialValue, value); }
        }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int AverageBits
        {
            get { return _averageBits; }
            set { SetProperty(ref _averageBits, value); }
        }
        /// <summary>
        /// TODO: documentation (what does this do anyways?)
        /// </summary>
        public int OversampleBits
        {
            get { return _oversampleBits; }
            set { SetProperty(ref _oversampleBits, value); }
        }
        /// <summary>
        /// The conversion ratio defined as normalized analog units per user-defined units
        /// </summary>
        public double ConversionRatio
        {
            get { return _conversionRatio; }
            set { SetProperty(ref _conversionRatio, value); }
        }
        /// <summary>
        /// The offset in normalized analog units
        /// </summary>
        public double SensorOffset
        {
            get { return _sensorOffset; }
            set { SetProperty(ref _sensorOffset, value); }
        }

        #region Private Fields
        private string _name = "OzAnalogInputData";
        private uint _iD;
        private int _accumulatorCenter;
        private int _accumulatorDeadband;
        private long _accumulatorInitialValue;
        private int _averageBits;
        private int _oversampleBits;
        private double _conversionRatio;
        private double _sensorOffset;
        #endregion
    }
}
