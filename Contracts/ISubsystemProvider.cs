using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        IObservableCollection<IPWMOutputProvider> PWMOutputs { get; }
        /// <summary>
        /// This subsystem's Analog Inputs
        /// </summary>
        IObservableCollection<IAnalogInputProvider> AnalogInputs { get; }
        /// <summary>
        /// This subsystem's Quadrature Encoders
        /// </summary>
        IObservableCollection<IQuadEncoderProvider> QuadEncoders { get; }
        /// <summary>
        /// This subsystem's Digital Inputs
        /// </summary>
        IObservableCollection<IDigitalInputProvider> DigitalInputs { get; }
        /// <summary>
        /// This subsystem's Relay Outputs
        /// </summary>
        IObservableCollection<IRelayOutputProvider> RelayOutputs { get; }
        /// <summary>
        /// This subsystem's CAN Talons
        /// </summary>
        IObservableCollection<ICANTalonProvider> CANTalons { get; }
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

        /// <summary>
        /// Adds a model instance of a PWMOutput
        /// </summary>
        /// <param name="pwmOutput">the PWMOutput model instance</param>
        void AddPWMOutput(PWMOutput pwmOutput);
        /// <summary>
        /// Adds a model instance of a DigitalInput
        /// </summary>
        /// <param name="digitalInput">the DigitalInput model instance</param>
        void AddDigitalInput(DigitalInput digitalInput);
        /// <summary>
        /// Adds a model instance of a AnalogInput
        /// </summary>
        /// <param name="analogInput">the AnalogInput model instance</param>
        void AddAnalogInput(AnalogInput analogInput);
        /// <summary>
        /// Adds a model instance of a QuadEncoder
        /// </summary>
        /// <param name="quadEncoder">the QuadEncoder model instance</param>
        void AddQuadEncoder(QuadEncoder quadEncoder);
        /// <summary>
        /// Adds a model instance of a RelayOutput
        /// </summary>
        /// <param name="relayOutput">the RelayOutput model instance</param>
        void AddRelayOutput(RelayOutput relayOutput);
        /// <summary>
        /// Adds a model instance of a CANTalon
        /// </summary>
        /// <param name="canTalon">the CANTalon model instance</param>
        void AddCANTalon(CANTalon canTalon);
    }
}
