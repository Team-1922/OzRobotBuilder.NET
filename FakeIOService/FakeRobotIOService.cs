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
            _pwmOutputIOServices[pwmOutput.ID] = new FakePWMOutputIOService(pwmOutput);
        }

        public void AddAnalogInput(AnalogInput analogInput)
        {
            _analogInputIOServices[analogInput.ID] = new FakeAnalogInputIOService(analogInput);
        }

        public void AddRelayOutput(RelayOutput relayOutput)
        {
            _relayOutputIOServices[relayOutput.ID] = new FakeRelayOutputIOService(relayOutput);
        }

        public void AddDigitalInput(DigitalInput digitalInput)
        {
            _digitalInputIOServices[digitalInput.ID] = new FakeDigitalInputIOService(digitalInput);
        }

        public void AddQuadEncoder(QuadEncoder quadEncoder)
        {
            _digitalInputIOServices[quadEncoder.ID] = _digitalInputIOServices[quadEncoder.ID1] = new FakeQuadEncoderIOService(quadEncoder);
        }

        public void AddCANTalon(CANTalon canTalon)
        {
            _canTalonIOServices[canTalon.ID] = new FakeCANTalonIOService(canTalon);
        }

        public void AddJoystick(Joystick joystick)
        {
            _joystickIOServices[joystick.ID] = new FakeJoystickIOService(joystick);
        }

        public void RemoveAnalogInput(int id)
        {
            _analogInputIOServices[id] = null;
        }

        public void RemoveCANTalon(int id)
        {
            _canTalonIOServices[id] = null;
        }

        public void RemoveDigitalInput(int id)
        {
            _digitalInputIOServices[id] = null;
        }

        public void RemoveQuadEncoder(int id0, int id1)
        {
            _digitalInputIOServices[id0] = _digitalInputIOServices[id1] = null;
        }

        public void RemovePWMOutput(int id)
        {
            _pwmOutputIOServices[id] = null;
        }

        public void RemoveRelayOutput(int id)
        {
            _relayOutputIOServices[id] = null;
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
