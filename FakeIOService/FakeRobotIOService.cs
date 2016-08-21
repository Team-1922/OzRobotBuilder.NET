using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.FakeIOService
{
    public class FakeRobotIOService : IRobotIOService
    {
        #region IRobotIOService
        public IReadOnlyDictionary<int, IAnalogInputIOService> AnalogInputs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyDictionary<int, ICANTalonIOService> CANTalons
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyDictionary<int, IInputService> DigitalInputs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyDictionary<int, IJoystickIOService> Joysticks
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyDictionary<int, IPWMOutputIOService> PWMOutputs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyDictionary<int, IRelayOutputIOService> RelayOutputs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void SetRobot(Robot robotModel)
        {
            //TODO: go through each item in the model and add them to the appropriate dictionaries
        }

        public void AddPWMOutput(PWMOutput pwmOutput)
        {
            var service = new FakePWMOutputIOService();
            service.Value = pwmOutput.Value;
            _pwmOutputIOServices[pwmOutput.ID] = service;
        }

        public void AddAnalogInput(AnalogInput analogInput)
        {
            throw new NotImplementedException();
        }

        public void AddRelayOutput(RelayOutput relayOutput)
        {
            var service = new FakeRelayOutputIOService();
            service.ValueAsBool = relayOutput.GetForwardValueBool();
            service.ReverseValueAsBool = relayOutput.GetReverseValueBool();
            _relayOutputIOServices[relayOutput.ID] = service;
        }

        public void AddDigitalInput(DigitalInput digitalInput)
        {
            var service = new FakeDigitalInputIOService();
            service.ValueAsBool = digitalInput.Value;
            _digitalInputIOServices[digitalInput.ID] = service;
        }

        public void AddQuadEncoder(QuadEncoder quadEncoder)
        {
            var service = new FakeQuadEncoderIOService();
            service.Value = quadEncoder.RawValue;
            service.Velocity = quadEncoder.RawVelocity;
            _digitalInputIOServices[quadEncoder.ID] = service;
            _digitalInputIOServices[quadEncoder.ID1] = service;
        }

        public void AddCANTalon(CANTalon canTalon)
        {
            throw new NotImplementedException();
        }

        public void AddJoystick(Joystick joystick)
        {
            var service = new FakeJoystickIOService();
        }

        public void RemoveAnalogInput(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveCANTalon(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveDigitalInput(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveQuadEncoder(int id0, int id1)
        {
            throw new NotImplementedException();
        }

        public void RemovePWMOutput(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRelayOutput(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Fields
        Dictionary<int, IAnalogInputIOService> _analogInputIOServices = new Dictionary<int, IAnalogInputIOService>();
        Dictionary<int, ICANTalonIOService> _canTalonIOServices = new Dictionary<int, ICANTalonIOService>();
        Dictionary<int, IInputService> _digitalInputIOServices = new Dictionary<int, IInputService>();
        Dictionary<int, IPWMOutputIOService> _pwmOutputIOServices = new Dictionary<int, IPWMOutputIOService>();
        Dictionary<int, IRelayOutputIOService> _relayOutputIOServices = new Dictionary<int, IRelayOutputIOService>();

        Dictionary<int, IJoystickIOService> _joystickIOServices = new Dictionary<int, IJoystickIOService>();
        #endregion
    }
}
