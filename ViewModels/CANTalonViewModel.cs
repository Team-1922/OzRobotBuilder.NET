using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;
using Team1922.MVVM.Services;

namespace Team1922.MVVM.ViewModels
{
    /// <summary>
    /// The viewmodel for each CANTalon instance
    /// </summary>
    internal class CANTalonViewModel : ViewModelBase, ICANTalonProvider
    {
        public CANTalonViewModel(ISubsystemProvider parent) : base(parent)
        {
        }

        /// <summary>
        /// The CANTalon model reference
        /// </summary>
        protected CANTalon _canTalonModel;

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
        /// The PIDConfig0 property of the CANTalon model
        /// </summary>

        public IPIDControllerSRXProvider PIDConfig0
        {
            get
            {
                return _pidConfig0Provider;
            }
        }

        /// <summary>
        /// The PIDConfig1 property of the CANTalon model
        /// </summary>
        public IPIDControllerSRXProvider PIDConfig1
        {
            get
            {
                return _pidConfig1Provider;
            }
        }

        /// <summary>
        /// The ForwardLimitSwitch property of the CANTalon model
        /// </summary>
        public bool ForwardLimitSwitch
        {
            get
            {
                return _canTalonModel.ForwardLimitSwitch;
            }

            private set
            {
                var temp = _canTalonModel.ForwardLimitSwitch;
                SetProperty(ref temp, value);
                _canTalonModel.ForwardLimitSwitch = temp;
            }
        }

        /// <summary>
        /// The ReverseLimitSwitch property of the CANTalon model
        /// </summary>
        public bool ReverseLimitSwitch
        {
            get
            {
                return _canTalonModel.ReverseLimitSwitch;
            }

            private set
            {
                var temp = _canTalonModel.ReverseLimitSwitch;
                SetProperty(ref temp, value);
                _canTalonModel.ReverseLimitSwitch = temp;
            }
        }

        /// <summary>
        /// The ForwardSoftLimitTripped property of the CANTalon model
        /// </summary>
        public bool ForwardSoftLimitTripped
        {
            get
            {
                return _canTalonModel.ForwardSoftLimitTripped;
            }

            private set
            {
                var temp = _canTalonModel.ForwardSoftLimitTripped;
                SetProperty(ref temp, value);
                _canTalonModel.ForwardSoftLimitTripped = temp;
            }
        }

        /// <summary>
        /// The ReverseSoftLimitTripped property of the CANTalon model
        /// </summary>
        public bool ReverseSoftLimitTripped
        {
            get
            {
                return _canTalonModel.ReverseSoftLimitTripped;
            }

            private set
            {
                var temp = _canTalonModel.ReverseSoftLimitTripped;
                SetProperty(ref temp, value);
                _canTalonModel.ReverseSoftLimitTripped = temp;
            }
        }

        /// <summary>
        /// The ControlMode property of the CANTalon model
        /// </summary>
        public CANTalonControlMode ControlMode
        {
            get
            {
                return _canTalonModel.ControlMode;
            }

            set
            {
                var temp = _canTalonModel.ControlMode;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ControlMode = value);
                _canTalonModel.ControlMode = temp;
            }
        }

        /// <summary>
        /// The EnabledPIDProfile property of the CANTalon model
        /// </summary>
        public bool EnabledPIDProfile
        {
            get
            {
                return _canTalonModel.EnabledPIDProfile;
            }

            set
            {
                var temp = _canTalonModel.EnabledPIDProfile;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].EnabledPIDProfile = value);
                _canTalonModel.EnabledPIDProfile = temp;
            }
        }

        /// <summary>
        /// The FeedbackDevice property of the CANTalon model
        /// </summary>
        public CANTalonFeedbackDevice FeedbackDevice
        {
            get
            {
                return _canTalonModel.FeedbackDevice;
            }

            set
            {
                var temp = _canTalonModel.FeedbackDevice;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].FeedbackDevice = value);
                _canTalonModel.FeedbackDevice = temp;
            }
        }

        /// <summary>
        /// The ForwardLimitSwitchEnabled property of the CANTalon model
        /// </summary>
        public bool ForwardLimitSwitchEnabled
        {
            get
            {
                return _canTalonModel.ForwardLimitSwitchEnabled;
            }

            set
            {
                var temp = _canTalonModel.ForwardLimitSwitchEnabled;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ForwardLimitSwitchEnabled = value);
                _canTalonModel.ForwardLimitSwitchEnabled = temp;
            }
        }

        /// <summary>
        /// The ForwardSoftLimit property of the CANTalon model
        /// </summary>
        public double ForwardSoftLimit
        {
            get
            {
                return _canTalonModel.ForwardSoftLimit;
            }

            set
            {
                var temp = _canTalonModel.ForwardSoftLimit;
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
                _canTalonModel.ForwardSoftLimit = temp;
            }
        }

        /// <summary>
        /// The ForwardSoftLimitEnabled property of the CANTalon model
        /// </summary>
        public bool ForwardSoftLimitEnabled
        {
            get
            {
                return _canTalonModel.ForwardSoftLimitEnabled;
            }

            set
            {
                var temp = _canTalonModel.ForwardSoftLimitEnabled;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ForwardSoftLimitEnabled = value);
                _canTalonModel.ForwardSoftLimitEnabled = temp;
            }
        }

        /// <summary>
        /// The ID property of the CANTalon model
        /// </summary>
        public int ID
        {
            get
            {
                return _canTalonModel.ID;
            }

            set
            {
                var temp = _canTalonModel.ID;
                SetProperty(ref temp, value);
                _canTalonModel.ID = temp;
            }
        }

        /// <summary>
        /// The NeutralMode property of the CANTalon model
        /// </summary>
        public CANTalonNeutralMode NeutralMode
        {
            get
            {
                return _canTalonModel.NeutralMode;
            }

            set
            {
                var temp = _canTalonModel.NeutralMode;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].NeutralMode = value);
                _canTalonModel.NeutralMode = temp;
            }
        }

        /// <summary>
        /// The NominalForwardVoltage property of the CANTalon model
        /// </summary>
        public double NominalForwardVoltage
        {
            get
            {
                return _canTalonModel.NominalForwardVoltage;
            }

            set
            {
                var temp = _canTalonModel.NominalForwardVoltage;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].NominalForwardVoltage = value);
                _canTalonModel.NominalForwardVoltage = temp;
            }
        }

        /// <summary>
        /// The NominalReverseVoltage property of the CANTalon model
        /// </summary>
        public double NominalReverseVoltage
        {
            get
            {
                return _canTalonModel.NominalReverseVoltage;
            }

            set
            {
                var temp = _canTalonModel.NominalReverseVoltage;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].NominalReverseVoltage = value);
                _canTalonModel.NominalReverseVoltage = temp;
            }
        }

        /// <summary>
        /// The PeakForwardVoltage property of the CANTalon model
        /// </summary>
        public double PeakForwardVoltage
        {
            get
            {
                return _canTalonModel.PeakForwardVoltage;
            }

            set
            {
                var temp = _canTalonModel.PeakForwardVoltage;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].PeakForwardVoltage = value);
                _canTalonModel.PeakForwardVoltage = temp;
            }
        }

        /// <summary>
        /// The PeakReverseVoltage property of the CANTalon model
        /// </summary>
        public double PeakReverseVoltage
        {
            get
            {
                return _canTalonModel.PeakReverseVoltage;
            }

            set
            {
                var temp = _canTalonModel.PeakReverseVoltage;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].PeakReverseVoltage = value);
                _canTalonModel.PeakReverseVoltage = temp;
            }
        }

        /// <summary>
        /// The ReverseClosedLoopOutput property of the CANTalon model
        /// </summary>
        public bool ReverseClosedLoopOutput
        {
            get
            {
                return _canTalonModel.ReverseClosedLoopOutput;
            }

            set
            {
                var temp = _canTalonModel.ReverseClosedLoopOutput;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ReverseClosedLoopOutput = value);
                _canTalonModel.ReverseClosedLoopOutput = temp;
            }
        }

        /// <summary>
        /// The ReverseLimitSwitchEnabled property of the CANTalon model
        /// </summary>
        public bool ReverseLimitSwitchEnabled
        {
            get
            {
                return _canTalonModel.ReverseLimitSwitchEnabled;
            }

            set
            {
                var temp = _canTalonModel.ReverseLimitSwitchEnabled;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ReverseLimitSwitchEnabled = value);
                _canTalonModel.ReverseLimitSwitchEnabled = temp;
            }
        }

        /// <summary>
        /// The ReversePercentVBusOutput property of the CANTalon model
        /// </summary>
        public bool ReversePercentVBusOutput
        {
            get
            {
                return _canTalonModel.ReversePercentVBusOutput;
            }

            set
            {
                var temp = _canTalonModel.ReversePercentVBusOutput;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ReversePercentVBusOutput = value);
                _canTalonModel.ReversePercentVBusOutput = temp;
            }
        }

        /// <summary>
        /// The ReverseSensor property of the CANTalon model
        /// </summary>
        public bool ReverseSensor
        {
            get
            {
                return _canTalonModel.ReverseSensor;
            }

            set
            {
                var temp = _canTalonModel.ReverseSensor;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ReverseSensor = value);
                _canTalonModel.ReverseSensor = temp;
            }
        }

        /// <summary>
        /// The ReverseSoftLimit property of the CANTalon model
        /// </summary>
        public double ReverseSoftLimit
        {
            get
            {
                return _canTalonModel.ReverseSoftLimit;
            }

            set
            {
                var temp = _canTalonModel.ReverseSoftLimit;
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
                _canTalonModel.ReverseSoftLimit = temp;
            }
        }

        /// <summary>
        /// The ReverseSoftLimitEnabled property of the CANTalon model
        /// </summary>
        public bool ReverseSoftLimitEnabled
        {
            get
            {
                return _canTalonModel.ReverseSoftLimitEnabled;
            }

            set
            {
                var temp = _canTalonModel.ReverseSoftLimitEnabled;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ReverseSoftLimitEnabled = value);
                _canTalonModel.ReverseSoftLimitEnabled = temp;
            }
        }

        /// <summary>
        /// The Value property of the CANTalon model
        /// </summary>
        public double Value
        {
            get
            {
                return _canTalonModel.Value;
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
                var temp = _canTalonModel.Value;
                SetProperty(ref temp, value);
                _canTalonModel.Value = temp;
            }
        }

        /// <summary>
        /// The ZeroSensorPositionOnIndexEnabled property of the CANTalon model
        /// </summary>
        public bool ZeroSensorPositionOnIndexEnabled
        {
            get
            {
                return _canTalonModel.ZeroSensorPositionOnIndexEnabled;
            }

            set
            {
                var temp = _canTalonModel.ZeroSensorPositionOnIndexEnabled;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ZeroSensorPositionOnIndexEnabled = value);
                _canTalonModel.ZeroSensorPositionOnIndexEnabled = temp;
            }
        }

        /// <summary>
        /// The ZeroSensorPositionOnRisingEdge property of the CANTalon model
        /// </summary>
        public bool ZeroSensorPositionOnRisingEdge
        {
            get
            {
                return _canTalonModel.ZeroSensorPositionOnRisingEdge;
            }

            set
            {
                var temp = _canTalonModel.ZeroSensorPositionOnRisingEdge;
                SetProperty(ref temp, IOService.Instance.CANTalons[ID].ZeroSensorPositionOnRisingEdge = value);
                _canTalonModel.ZeroSensorPositionOnRisingEdge = temp;
            }
        }

        /// <summary>
        /// Set the CANTalon model reference
        /// </summary>
        public void SetCANTalon(CANTalon canTalon)
        {
            _canTalonModel = canTalon;

            _quadEncoderProvider = null;
            _analogInputProvider = null;
            _pidConfig0Provider = null;
            _pidConfig1Provider = null;

            if (null != _canTalonModel.QuadEncoder)
            {
                _quadEncoderProvider = new CANTalonQuadEncoderViewModel(this);
                _quadEncoderProvider.SetCANTalon(canTalon);
            }
            if (null != _canTalonModel.AnalogInput)
            {
                _analogInputProvider = new CANTalonAnalogInputViewModel(this);
                _analogInputProvider.SetCANTalon(canTalon);
            }
            if (null != _canTalonModel.PIDConfig0)
            {
                _pidConfig0Provider = new PIDControllerSRXViewModel(this);
                _pidConfig0Provider.SetPIDController(canTalon.PIDConfig0);
            }
            if (null != _canTalonModel.PIDConfig1)
            {
                _pidConfig1Provider = new PIDControllerSRXViewModel(this);
                _pidConfig1Provider.SetPIDController(canTalon.PIDConfig1);
            }
        }
        #endregion

        #region ICompoundProvider
        public IEnumerable<IProvider> Children
        {
            get
            {
                return _children.Values;
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
        public string Name
        {
            get
            {
                return _canTalonModel.Name;
            }

            set
            {
                var temp = _canTalonModel.Name;
                SetProperty(ref temp, value);
                _canTalonModel.Name = temp;
            }
        }
        public string GetModelJson()
        {
            return JsonSerialize(_canTalonModel);
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
                case "PIDConfig0":
                    return _pidConfig0Provider.GetModelJson();
                case "PIDConfig1":
                    return _pidConfig1Provider.GetModelJson();
                case "QuadEncoder":
                    return _quadEncoderProvider.GetModelJson();
                case "AnalogInput":
                    return _analogInputProvider.GetModelJson();

                case "ControlMode":
                    return ControlMode.ToString();
                case "EnabledPIDProfile":
                    return EnabledPIDProfile.ToString();
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
                case "PIDConfig0":
                    _pidConfig0Provider.SetModelJson(value);
                    break;
                case "PIDConfig1":
                    _pidConfig1Provider.SetModelJson(value);
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
                case "EnabledPIDProfile":
                    EnabledPIDProfile = SafeCastBool(value);
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

        public override string ModelTypeName
        {
            get
            {
                var brokenName = _canTalonModel.GetType().ToString().Split('.');
                return brokenName[brokenName.Length - 1];
            }
        }

        #endregion

        #region Private Fields
        Dictionary<string, IProvider> _children = new Dictionary<string, IProvider>();

        ICANTalonQuadEncoderProvider _quadEncoderProvider
        {
            get
            {
                return _children["_quadEncoderProvider"] as ICANTalonQuadEncoderProvider;
            }

            set
            {
                _children["_quadEncoderProvider"] = value;
            }
        }
        ICANTalonAnalogInputProvider _analogInputProvider
        {
            get
            {
                return _children["_analogInputProvider"] as ICANTalonAnalogInputProvider;
            }

            set
            {
                _children["_analogInputProvider"] = value;
            }
        }
        IPIDControllerSRXProvider _pidConfig0Provider
        {
            get
            {
                return _children["_pidConfig0Provider"] as IPIDControllerSRXProvider;
            }

            set
            {
                _children["_pidConfig0Provider"] = value;
            }
        }
        IPIDControllerSRXProvider _pidConfig1Provider
        {
            get
            {
                return _children["_pidConfig1Provider"] as IPIDControllerSRXProvider;
            }

            set
            {
                _children["_pidConfig1Provider"] = value;
            }
        }
        #endregion
    }
}