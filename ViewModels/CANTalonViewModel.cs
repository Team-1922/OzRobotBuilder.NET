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
    internal class CANTalonViewModel : ICANTalonProvider
    {
        /// <summary>
        /// The CANTalon model reference
        /// </summary>
        protected CANTalon _canTalonModel;


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
                _canTalonModel.ControlMode = IOService.Instance.CANTalons[ID].ControlMode = value;
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
                _canTalonModel.EnabledPIDProfile = IOService.Instance.CANTalons[ID].EnabledPIDProfile = value;
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
                _canTalonModel.FeedbackDevice = IOService.Instance.CANTalons[ID].FeedbackDevice = value;
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
                _canTalonModel.ForwardLimitSwitchEnabled = IOService.Instance.CANTalons[ID].ForwardLimitSwitchEnabled = value;
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
                _canTalonModel.ForwardSoftLimit = IOService.Instance.CANTalons[ID].ForwardSoftLimit = value;
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
                _canTalonModel.ForwardSoftLimitEnabled = IOService.Instance.CANTalons[ID].ForwardSoftLimitEnabled = value;
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
                _canTalonModel.ID = value;
            }
        }

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
                _canTalonModel.Name = value;
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
                _canTalonModel.NeutralMode = IOService.Instance.CANTalons[ID].NeutralMode = value;
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
                _canTalonModel.NominalForwardVoltage = IOService.Instance.CANTalons[ID].NominalForwardVoltage = value;
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
                _canTalonModel.NominalReverseVoltage = IOService.Instance.CANTalons[ID].NominalReverseVoltage = value;
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
                _canTalonModel.PeakForwardVoltage = IOService.Instance.CANTalons[ID].PeakForwardVoltage = value;
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
                _canTalonModel.PeakReverseVoltage = IOService.Instance.CANTalons[ID].PeakReverseVoltage = value;
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
                _canTalonModel.ReverseClosedLoopOutput = IOService.Instance.CANTalons[ID].ReverseClosedLoopOutput = value;
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
                _canTalonModel.ReverseLimitSwitchEnabled = IOService.Instance.CANTalons[ID].ReverseLimitSwitchEnabled = value;
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
                _canTalonModel.ReversePercentVBusOutput = IOService.Instance.CANTalons[ID].ReversePercentVBusOutput = value;
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
                _canTalonModel.ReverseSensor = IOService.Instance.CANTalons[ID].ReverseSensor = value;
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
                _canTalonModel.ReverseSoftLimit = IOService.Instance.CANTalons[ID].ReverseSoftLimit = value;
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
                _canTalonModel.ReverseSoftLimitEnabled = IOService.Instance.CANTalons[ID].ReverseSoftLimitEnabled = value;
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
                _canTalonModel.Value = value;
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
                                IOService.Instance.CANTalons[ID].Value = (Value - AnalogInput.SensorOffset) / AnalogInput.ConversionRatio;
                                break;
                            case CANTalonFeedbackDevice.QuadEncoder:
                                IOService.Instance.CANTalons[ID].Value = (Value - QuadEncoder.SensorOffset) / QuadEncoder.ConversionRatio;
                                break;
                        }
                        break;
                }
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
                _canTalonModel.ZeroSensorPositionOnIndexEnabled = IOService.Instance.CANTalons[ID].ZeroSensorPositionOnIndexEnabled = value;
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
                _canTalonModel.ZeroSensorPositionOnRisingEdge = IOService.Instance.CANTalons[ID].ZeroSensorPositionOnRisingEdge = value;
            }
        }

        public IEnumerable<IProvider> Children
        {
            get
            {
                return _children.Values;
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
                _quadEncoderProvider = new CANTalonQuadEncoderViewModel();
                _quadEncoderProvider.SetCANTalon(canTalon);
            }
            if (null != _canTalonModel.AnalogInput)
            {
                _analogInputProvider = new CANTalonAnalogInputViewModel();
                _analogInputProvider.SetCANTalon(canTalon);
            }
            if (null != _canTalonModel.PIDConfig0)
            {
                _pidConfig0Provider = new PIDControllerSRXViewModel();
                _pidConfig0Provider.SetPIDController(canTalon.PIDConfig0);
            }
            if (null != _canTalonModel.PIDConfig1)
            {
                _pidConfig1Provider = new PIDControllerSRXViewModel();
                _pidConfig1Provider.SetPIDController(canTalon.PIDConfig1);
            }
        }

        /// <summary>
        /// Called every update cycle to update input values from the <see cref="IRobotIOService"/>
        /// </summary>
        public void UpdateInputValues()
        {
            _quadEncoderProvider.UpdateInputValues();
            _analogInputProvider.UpdateInputValues();

            // the "value" value (this might change depending on what mode you're in)
            _canTalonModel.Value = IOService.Instance.CANTalons[ID].Value;
            _canTalonModel.ForwardLimitSwitch = IOService.Instance.CANTalons[ID].ForwardLimitSwitch;
            _canTalonModel.ReverseLimitSwitch = IOService.Instance.CANTalons[ID].ReverseLimitSwitch;
        }

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