﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The interface for subsystem viewmodels
    /// </summary>
    public interface ISubsystemProvider : IInputProvider, ICompoundProvider
    {
        /// <summary>
        /// This subsystem's PWM Outputs
        /// </summary>
        IEnumerable<IPWMOutputProvider> PWMOutputs { get; }
        /// <summary>
        /// This subsystem's Analog Inputs
        /// </summary>
        IEnumerable<IAnalogInputProvider> AnalogInputs { get; }
        /// <summary>
        /// This subsystem's Quadrature Encoders
        /// </summary>
        IEnumerable<IQuadEncoderProvider> QuadEncoders { get; }
        /// <summary>
        /// This subsystem's Relay Outputs
        /// </summary>
        IEnumerable<IRelayOutputProvider> RelayOutputs { get; }
        /// <summary>
        /// This subsystem's CAN Talons
        /// </summary>
        IEnumerable<ICANTalonProvider> CANTalons { get; }
        /// <summary>
        /// This subsystem's PID Controller (if enabled)
        /// </summary>
        IPIDControllerSoftwareProvider PIDController { get; }
        /// <summary>
        /// The unique identifier for this subsystem
        /// </summary>
        int ID { get; }
        /// <summary>
        /// Whether this subsystem has a software PID controller.  This is not recommended, but is supported
        /// </summary>
        bool SoftwarePIDEnabled { get; set; }
        
        /// <summary>
        /// Sets the model instance of this subsystem provider
        /// </summary>
        /// <param name="subsystem">the subsystem model instance</param>
        void SetSubsystem(Subsystem subsystem);
    }
}
