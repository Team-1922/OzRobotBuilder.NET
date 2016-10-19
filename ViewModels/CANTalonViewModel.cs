using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    /// <summary>
    /// The viewmodel for each CANTalon instance
    /// </summary>
    internal class CANTalonViewModel : CompoundViewModelBase<CANTalon>, ICANTalonProvider
    {
        public CANTalonViewModel(IProvider parent) : base(parent)
        {
            _quadEncoderProvider = new CANTalonQuadEncoderViewModel(this);
            _analogInputProvider = new CANTalonAnalogInputViewModel(this);
            _pidConfigProvider = new PIDControllerSRXViewModel(this);
        }
        
        #region ICANTalonProvider
        /// <summary>
        /// The QuadEncoder property of the CANTalon model
        /// </summary>
        public ICANTalonQuadEncoderProvider QuadEncoder
        {
            get
            {
                return _quadEncoderProvider;
            }
        }

        /// <summary>
        /// The AnalogInput property of the CANTalon model
        /// </summary>
        public ICANTalonAnalogInputProvider AnalogInput
        {
            get
            {
                return _analogInputProvider;
            }
        }
        /// <summary>
        /// The PIDConfig property of the CANTalon model
        /// </summary>

        public IPIDControllerSRXProvider PIDConfig
        {
            get
            {
                return _pidConfigProvider;
            }
        }

        /// <summary>
        /// The ForwardLimitSwitch property of the CANTalon model
        /// </summary>
        public bool ForwardLimitSwitch
        {
            get
            {
                return ModelReference.ForwardLimitSwitch;
            }

            private set
            {
                var temp = ModelReference.ForwardLimitSwitch;
                SetProperty(ref temp, value);
                ModelReference.ForwardLimitSwitch = temp;
            }
        }

        /// <summary>
        /// The ReverseLimitSwitch property of the CANTalon model
        /// </summary>
        public bool ReverseLimitSwitch
        {
            get
            {
                return ModelReference.ReverseLimitSwitch;
            }

            private set
            {
                var temp = ModelReference.ReverseLimitSwitch;
                SetProperty(ref temp, value);
                ModelReference.ReverseLimitSwitch = temp;
            }
        }

        /// <summary>
        /// The ForwardSoftLimitTripped property of the CANTalon model
        /// </summary>
        public bool ForwardSoftLimitTripped
        {
            get
            {
                return ModelReference.ForwardSoftLimitTripped;
            }

            private set
            {
                var temp = ModelReference.ForwardSoftLimitTripped;
                SetProperty(ref temp, value);
                ModelReference.ForwardSoftLimitTripped = temp;
            }
        }

        /// <summary>
        /// The ReverseSoftLimitTripped property of the CANTalon model
        /// </summary>
        public bool ReverseSoftLimitTripped
        {
            get
            {
                return ModelReference.ReverseSoftLimitTripped;
            }

            private set
            {
                var temp = ModelReference.ReverseSoftLimitTripped;
                SetProperty(ref temp, value);
                ModelReference.ReverseSoftLimitTripped = temp;
            }
        }

        /// <summary>
        /// The ControlMode property of the CANTalon model
        /// </summary>
        public CANTalonControlMode ControlMode
        {
            get
            {
                return ModelReference.ControlMode;
            }

            set
            {
                var temp = ModelReference.ControlMode;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ControlMode = value);
                ModelReference.ControlMode = temp;
            }
        }

        /// <summary>
        /// The FeedbackDevice property of the CANTalon model
        /// </summary>
        public CANTalonFeedbackDevice FeedbackDevice
        {
            get
            {
                return ModelReference.FeedbackDevice;
            }

            set
            {
                var temp = ModelReference.FeedbackDevice;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].FeedbackDevice = value);
                ModelReference.FeedbackDevice = temp;
            }
        }

        /// <summary>
        /// The ForwardLimitSwitchEnabled property of the CANTalon model
        /// </summary>
        public bool ForwardLimitSwitchEnabled
        {
            get
            {
                return ModelReference.ForwardLimitSwitchEnabled;
            }

            set
            {
                var temp = ModelReference.ForwardLimitSwitchEnabled;
                SetProperty(ref temp, value);
                ModelReference.ForwardLimitSwitchEnabled = temp;
                IOService.Instance.CANTalons[ID].EnableLimitSwitches(value, ReverseLimitSwitchEnabled);
            }
        }

        /// <summary>
        /// The ForwardSoftLimit property of the CANTalon model
        /// </summary>
        public double ForwardSoftLimit
        {
            get
            {
                return ModelReference.ForwardSoftLimit;
            }

            set
            {
                var temp = ModelReference.ForwardSoftLimit;
                switch(FeedbackDevice) //TODO: this might need some refining
                {
                    case CANTalonFeedbackDevice.AnalogEncoder:
                    case CANTalonFeedbackDevice.AnalogPotentiometer:
                        IOService.Instance.CANTalons[ID].ForwardSoftLimit = (value - AnalogInput.SensorOffset) / AnalogInput.ConversionRatio;
                        break;
                    default:
                        IOService.Instance.CANTalons[ID].ForwardSoftLimit = (value - QuadEncoder.SensorOffset) / QuadEncoder.ConversionRatio;
                        break;
                }
                SetProperty(ref temp, value);
                ModelReference.ForwardSoftLimit = temp;
            }
        }

        /// <summary>
        /// The ForwardSoftLimitEnabled property of the CANTalon model
        /// </summary>
        public bool ForwardSoftLimitEnabled
        {
            get
            {
                return ModelReference.ForwardSoftLimitEnabled;
            }

            set
            {
                var temp = ModelReference.ForwardSoftLimitEnabled;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ForwardSoftLimitEnabled = value);
                ModelReference.ForwardSoftLimitEnabled = temp;
            }
        }

        /// <summary>
        /// The ID property of the CANTalon model
        /// </summary>
        public int ID
        {
            get
            {
                return ModelReference.ID;
            }

            set
            {
                var temp = ModelReference.ID;
                SetProperty(ref temp, value);
                ModelReference.ID = temp;
            }
        }

        /// <summary>
        /// The NeutralMode property of the CANTalon model
        /// </summary>
        public CANTalonNeutralMode NeutralMode
        {
            get
            {
                return ModelReference.NeutralMode;
            }

            set
            {
                var temp = ModelReference.NeutralMode;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].NeutralMode = value);
                ModelReference.NeutralMode = temp;
            }
        }

        /// <summary>
        /// The NominalForwardVoltage property of the CANTalon model
        /// </summary>
        public double NominalForwardVoltage
        {
            get
            {
                return ModelReference.NominalForwardVoltage;
            }

            set
            {
                var temp = ModelReference.NominalForwardVoltage;
                SetProperty(ref temp, value);
                ModelReference.NominalForwardVoltage = temp;
                IOService.Instance.CANTalons[ID].ConfigureNominalVoltage(value, NominalReverseVoltage);
            }
        }

        /// <summary>
        /// The NominalReverseVoltage property of the CANTalon model
        /// </summary>
        public double NominalReverseVoltage
        {
            get
            {
                return ModelReference.NominalReverseVoltage;
            }

            set
            {
                var temp = ModelReference.NominalReverseVoltage;
                SetProperty(ref temp, value);
                ModelReference.NominalReverseVoltage = temp;
                IOService.Instance.CANTalons[ID].ConfigureNominalVoltage(NominalForwardVoltage, value);
            }
        }

        /// <summary>
        /// The PeakForwardVoltage property of the CANTalon model
        /// </summary>
        public double PeakForwardVoltage
        {
            get
            {
                return ModelReference.PeakForwardVoltage;
            }

            set
            {
                var temp = ModelReference.PeakForwardVoltage;
                SetProperty(ref temp, value);
                ModelReference.PeakForwardVoltage = temp;
                IOService.Instance.CANTalons[ID].ConfigurePeakVoltage(value, PeakReverseVoltage);
            }
        }

        /// <summary>
        /// The PeakReverseVoltage property of the CANTalon model
        /// </summary>
        public double PeakReverseVoltage
        {
            get
            {
                return ModelReference.PeakReverseVoltage;
            }

            set
            {
                var temp = ModelReference.PeakReverseVoltage;
                SetProperty(ref temp, value);
                ModelReference.PeakReverseVoltage = temp;
                IOService.Instance.CANTalons[ID].ConfigurePeakVoltage(PeakForwardVoltage, value);
            }
        }

        /// <summary>
        /// The ReverseClosedLoopOutput property of the CANTalon model
        /// </summary>
        public bool ReverseClosedLoopOutput
        {
            get
            {
                return ModelReference.ReverseClosedLoopOutput;
            }

            set
            {
                var temp = ModelReference.ReverseClosedLoopOutput;
                SetProperty(ref temp, value);
                ModelReference.ReverseClosedLoopOutput = temp;
                IOService.Instance.CANTalons[ID].ReverseCloseLoopOutput = value;
            }
        }

        /// <summary>
        /// The ReverseLimitSwitchEnabled property of the CANTalon model
        /// </summary>
        public bool ReverseLimitSwitchEnabled
        {
            get
            {
                return ModelReference.ReverseLimitSwitchEnabled;
            }

            set
            {
                var temp = ModelReference.ReverseLimitSwitchEnabled;
                SetProperty(ref temp, value);
                ModelReference.ReverseLimitSwitchEnabled = temp;
                IOService.Instance.CANTalons[ID].EnableLimitSwitches(ForwardLimitSwitchEnabled, value);
            }
        }

        /// <summary>
        /// The ReversePercentVBusOutput property of the CANTalon model
        /// </summary>
        public bool ReversePercentVBusOutput
        {
            get
            {
                return ModelReference.ReversePercentVBusOutput;
            }

            set
            {
                var temp = ModelReference.ReversePercentVBusOutput;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ReversePercentVBusOutput = value);
                ModelReference.ReversePercentVBusOutput = temp;
            }
        }

        /// <summary>
        /// The ReverseSensor property of the CANTalon model
        /// </summary>
        public bool ReverseSensor
        {
            get
            {
                return ModelReference.ReverseSensor;
            }

            set
            {
                var temp = ModelReference.ReverseSensor;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ReverseSensor = value);
                ModelReference.ReverseSensor = temp;
            }
        }

        /// <summary>
        /// The ReverseSoftLimit property of the CANTalon model
        /// </summary>
        public double ReverseSoftLimit
        {
            get
            {
                return ModelReference.ReverseSoftLimit;
            }

            set
            {
                var temp = ModelReference.ReverseSoftLimit;
                switch (FeedbackDevice) //TODO: this might need some refining
                {
                    case CANTalonFeedbackDevice.AnalogEncoder:
                    case CANTalonFeedbackDevice.AnalogPotentiometer:
                        IOService.Instance.CANTalons[ID].ReverseSoftLimit = (value - AnalogInput.SensorOffset) / AnalogInput.ConversionRatio;
                        break;
                    default:
                        IOService.Instance.CANTalons[ID].ReverseSoftLimit = (value - QuadEncoder.SensorOffset) / QuadEncoder.ConversionRatio;
                        break;
                }
                SetProperty(ref temp, value);
                ModelReference.ReverseSoftLimit = temp;
            }
        }

        /// <summary>
        /// The ReverseSoftLimitEnabled property of the CANTalon model
        /// </summary>
        public bool ReverseSoftLimitEnabled
        {
            get
            {
                return ModelReference.ReverseSoftLimitEnabled;
            }

            set
            {
                var temp = ModelReference.ReverseSoftLimitEnabled;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ReverseSoftLimitEnabled = value);
                ModelReference.ReverseSoftLimitEnabled = temp;
            }
        }

        /// <summary>
        /// The Value property of the CANTalon model
        /// </summary>
        public double Value
        {
            get
            {
                return ModelReference.Value;
            }

            set
            {
                switch (ControlMode)
                {
                    case CANTalonControlMode.Current:
                        // TODO:
                        break;
                    case CANTalonControlMode.Follower:
                        // TODO:
                        break;
                    case CANTalonControlMode.MotionProfile:
                        // TODO:
                        break;
                    case CANTalonControlMode.PercentVbus:
                        IOService.Instance.CANTalons[ID].Value = TypeRestrictions.Clamp("PWMOutput.Value", value);
                        break;
                    case CANTalonControlMode.Voltage:
                        // TODO:
                        break;
                    case CANTalonControlMode.Position:
                    case CANTalonControlMode.Speed:
                        switch (FeedbackDevice)
                        {
                            case CANTalonFeedbackDevice.AnalogEncoder:
                            case CANTalonFeedbackDevice.AnalogPotentiometer:
                                IOService.Instance.CANTalons[ID].Value = (value - AnalogInput.SensorOffset) / AnalogInput.ConversionRatio;
                                break;
                            case CANTalonFeedbackDevice.QuadEncoder:
                                IOService.Instance.CANTalons[ID].Value = (value - QuadEncoder.SensorOffset) / QuadEncoder.ConversionRatio;
                                break;
                        }
                        break;
                }
                var temp = ModelReference.Value;
                SetProperty(ref temp, value);
                ModelReference.Value = temp;
            }
        }

        /// <summary>
        /// The ZeroSensorPositionOnIndexEnabled property of the CANTalon model
        /// </summary>
        public bool ZeroSensorPositionOnIndexEnabled
        {
            get
            {
                return ModelReference.ZeroSensorPositionOnIndexEnabled;
            }

            set
            {
                var temp = ModelReference.ZeroSensorPositionOnIndexEnabled;
                SetProperty(ref temp, value);
                ModelReference.ZeroSensorPositionOnIndexEnabled = temp;
                IOService.Instance.CANTalons[ID].EnableZeroSensorPositionOnIndex(value, ZeroSensorPositionOnRisingEdge);
            }
        }

        /// <summary>
        /// The ZeroSensorPositionOnRisingEdge property of the CANTalon model
        /// </summary>
        public bool ZeroSensorPositionOnRisingEdge
        {
            get
            {
                return ModelReference.ZeroSensorPositionOnRisingEdge;
            }

            set
            {
                var temp = ModelReference.ZeroSensorPositionOnRisingEdge;
                SetProperty(ref temp, value);
                ModelReference.ZeroSensorPositionOnRisingEdge = temp;
                IOService.Instance.CANTalons[ID].EnableZeroSensorPositionOnIndex(ZeroSensorPositionOnIndexEnabled, value);
            }
        }

        /// <summary>
        /// Set the CANTalon model reference
        /// </summary>
        public void SetCANTalon(CANTalon canTalon)
        {

        }
        #endregion

        #region ICompoundProvider
        public override IObservableCollection Children
        {
            get
            {
                return _children;
            }
        }
        #endregion

        #region IInputProvider
        /// <summary>
        /// Called every update cycle to update input values from the <see cref="IRobotIOService"/>
        /// </summary>
        public void UpdateInputValues()
        {
            _quadEncoderProvider.UpdateInputValues();
            _analogInputProvider.UpdateInputValues();
            
            ForwardLimitSwitch = IOService.Instance.CANTalons[ID].ForwardLimitSwitch;
            ReverseLimitSwitch = IOService.Instance.CANTalons[ID].ReverseLimitSwitch;

            ForwardSoftLimit = IOService.Instance.CANTalons[ID].ForwardSoftLimit;
            ReverseSoftLimit = IOService.Instance.CANTalons[ID].ReverseSoftLimit;
        }
        #endregion

        #region IProvider
        /// <summary>
        /// The Name property of the CANTalon model
        /// </summary>
        public override string Name
        {
            get
            {
                return ModelReference.Name;
            }

            set
            {
                var temp = ModelReference.Name;
                SetProperty(ref temp, value);
                ModelReference.Name = temp;
            }
        }
        public string GetModelJson()
        {
            return JsonSerialize(ModelReference);
        }
        public void SetModelJson(string text)
        {
            SetCANTalon(JsonDeserialize<CANTalon>(text));
        }
        #endregion

        #region ViewModelBase
        protected override string GetValue(string key)
        {
            switch (key)
            {
                case "PIDConfig":
                    return _pidConfigProvider.GetModelJson();
                case "QuadEncoder":
                    return _quadEncoderProvider.GetModelJson();
                case "AnalogInput":
                    return _analogInputProvider.GetModelJson();

                case "ControlMode":
                    return ControlMode.ToString();
                case "FeedbackDevice":
                    return FeedbackDevice.ToString();
                case "ForwardLimitSwitch":
                    return ForwardLimitSwitch.ToString();
                case "ForwardLimitSwitchEnabled":
                    return ForwardLimitSwitchEnabled.ToString();
                case "ForwardSoftLimit":
                    return ForwardSoftLimit.ToString();
                case "ForwardSoftLimitEnabled":
                    return ForwardSoftLimitEnabled.ToString();
                case "ForwardSoftLimitTripped":
                    return ForwardSoftLimitTripped.ToString();
                case "ID":
                    return ID.ToString();
                case "Name":
                    return Name;
                case "NeutralMode":
                    return NeutralMode.ToString();
                case "NominalForwardVoltage":
                    return NominalForwardVoltage.ToString();
                case "NominalReverseVoltage":
                    return NominalReverseVoltage.ToString();
                case "PeakForwardVoltage":
                    return PeakForwardVoltage.ToString();
                case "PeakReverseVoltage":
                    return PeakReverseVoltage.ToString();
                case "ReverseClosedLoopOutput":
                    return ReverseClosedLoopOutput.ToString();
                case "ReverseLimitSwitch":
                    return ReverseLimitSwitch.ToString();
                case "ReverseLimitSwitchEnabled":
                    return ReverseLimitSwitchEnabled.ToString();
                case "ReversePercentVBusOutput":
                    return ReversePercentVBusOutput.ToString();
                case "ReverseSensor":
                    return ReverseSensor.ToString();
                case "ReverseSoftLimit":
                    return ReverseSoftLimit.ToString();
                case "ReverseSoftLimitEnabled":
                    return ReverseSoftLimitEnabled.ToString();
                case "ReverseSoftLimitTripped":
                    return ReverseSoftLimitTripped.ToString();
                case "Value":
                    return Value.ToString();
                case "ZeroSensorPositionOnIndexEnabled":
                    return ZeroSensorPositionOnIndexEnabled.ToString();
                case "ZeroSensorPositionOnRisingEdge":
                    return ZeroSensorPositionOnRisingEdge.ToString();
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }
        protected override void SetValue(string key, string value)
        {
            switch (key)
            {
                case "PIDConfig":
                    _pidConfigProvider.SetModelJson(value);
                    break;
                case "QuadEncoder":
                    _quadEncoderProvider.SetModelJson(value);
                    break;
                case "AnalogInput":
                    _analogInputProvider.SetModelJson(value);
                    break;

                case "ControlMode":
                    ControlMode = SafeCastEnum<CANTalonControlMode>(value);
                    break;
                case "FeedbackDevice":
                    FeedbackDevice = SafeCastEnum<CANTalonFeedbackDevice>(value);
                    break;
                case "ForwardLimitSwitch":
                    ForwardLimitSwitch = SafeCastBool(value);
                    break;
                case "ForwardLimitSwitchEnabled":
                    ForwardLimitSwitchEnabled = SafeCastBool(value);
                    break;
                case "ForwardSoftLimit":
                    ForwardSoftLimit = SafeCastDouble(value);
                    break;
                case "ForwardSoftLimitEnabled":
                    ForwardSoftLimitEnabled = SafeCastBool(value);
                    break;
                case "ForwardSoftLimitTripped":
                    ForwardSoftLimitTripped = SafeCastBool(value);
                    break;
                case "ID":
                    ID = SafeCastInt(value);
                    break;
                case "Name":
                    Name = value;
                    break;
                case "NeutralMode":
                    NeutralMode = SafeCastEnum<CANTalonNeutralMode>(value);
                    break;
                case "NominalForwardVoltage":
                    NominalForwardVoltage = SafeCastDouble(value);
                    break;
                case "NominalReverseVoltage":
                    NominalReverseVoltage = SafeCastDouble(value);
                    break;
                case "PeakForwardVoltage":
                    PeakForwardVoltage = SafeCastDouble(value);
                    break;
                case "PeakReverseVoltage":
                    PeakReverseVoltage = SafeCastDouble(value);
                    break;
                case "ReverseClosedLoopOutput":
                    ReverseClosedLoopOutput = SafeCastBool(value);
                    break;
                case "ReverseLimitSwitch":
                    ReverseLimitSwitch = SafeCastBool(value);
                    break;
                case "ReverseLimitSwitchEnabled":
                    ReverseLimitSwitchEnabled = SafeCastBool(value);
                    break;
                case "ReversePercentVBusOutput":
                    ReversePercentVBusOutput = SafeCastBool(value);
                    break;
                case "ReverseSensor":
                    ReverseSensor = SafeCastBool(value);
                    break;
                case "ReverseSoftLimit":
                    ReverseSoftLimit = SafeCastDouble(value);
                    break;
                case "ReverseSoftLimitEnabled":
                    ReverseSoftLimitEnabled = SafeCastBool(value);
                    break;
                case "ReverseSoftLimitTripped":
                    ReverseSoftLimitTripped = SafeCastBool(value);
                    break;
                case "Value":
                    Value = SafeCastDouble(value);
                    break;
                case "ZeroSensorPositionOnIndexEnabled":
                    ZeroSensorPositionOnIndexEnabled = SafeCastBool(value);
                    break;
                case "ZeroSensorPositionOnRisingEdge":
                    ZeroSensorPositionOnRisingEdge = SafeCastBool(value);
                    break;
                default:
                    throw new ArgumentException($"\"{key}\" Is Inaccessible or Does Not Exist");
            }
        }
        protected override void OnModelChange()
        {
            _quadEncoderProvider = null;
            _analogInputProvider = null;
            _pidConfigProvider = null;

            if (null != ModelReference.QuadEncoder)
            {
                _quadEncoderProvider = new CANTalonQuadEncoderViewModel(this);
                _quadEncoderProvider.ModelReference = ModelReference.QuadEncoder;
            }
            if (null != ModelReference.AnalogInput)
            {
                _analogInputProvider = new CANTalonAnalogInputViewModel(this);
                _analogInputProvider.ModelReference = ModelReference.AnalogInput;
            }
            if (null != ModelReference.PIDConfig)
            {
                _pidConfigProvider = new PIDControllerSRXViewModel(this);
                _pidConfigProvider.ModelReference = ModelReference.PIDConfig;
            }
        }
        #endregion

        #region Private Fields
        ObservableCollection<IProvider> _children = new ObservableCollection<IProvider>() { null, null, null };
        ICANTalonQuadEncoderProvider _quadEncoderProvider
        {
            get
            {
                return _children[0] as ICANTalonQuadEncoderProvider;
            }

            set
            {
                _children[0] = value;
            }
        }
        ICANTalonAnalogInputProvider _analogInputProvider
        {
            get
            {
                return _children[1] as ICANTalonAnalogInputProvider;
            }

            set
            {
                _children[1] = value;
            }
        }
        IPIDControllerSRXProvider _pidConfigProvider
        {
            get
            {
                return _children[2] as IPIDControllerSRXProvider;
            }

            set
            {
                _children[2] = value;
            }
        }
        #endregion
    }
}