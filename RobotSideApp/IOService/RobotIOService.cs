using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace RobotSideApp.IOService
{
    class RobotIOService : IRobotIOService
    {
        #region IRobotIOService
        public IReadOnlyDictionary<int, IAnalogInputIOService> AnalogInputs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int AnalogInputSampleRate
        {
            get
            {
                throw new NotImplementedException();
            }

            set
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

        public void AddAnalogInput(AnalogInput analogInput)
        {
            throw new NotImplementedException();
        }

        public void AddCANTalon(CANTalon canTalon)
        {
            _canTalonIOServices.Add(canTalon.ID, new CANTalonIOService(canTalon));
        }

        public void AddDigitalInput(DigitalInput digitalInput)
        {
            throw new NotImplementedException();
        }

        public void AddJoystick(Joystick joystick)
        {
            throw new NotImplementedException();
        }

        public void AddPWMOutput(PWMOutput pwmOutput)
        {
            throw new NotImplementedException();
        }

        public void AddQuadEncoder(QuadEncoder quadEncoder)
        {
            throw new NotImplementedException();
        }

        public void AddRelayOutput(RelayOutput relayOutput)
        {
            throw new NotImplementedException();
        }

        public void RemoveAnalogInput(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveCANTalon(int id)
        {
            _canTalonIOServices.Remove(id);
        }

        public void RemoveDigitalInput(int id)
        {
            throw new NotImplementedException();
        }

        public void RemovePWMOutput(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveQuadEncoder(int id0, int id1)
        {
            throw new NotImplementedException();
        }

        public void RemoveRelayOutput(int id)
        {
            throw new NotImplementedException();
        }

        public void SetRobot(Robot robotModel)
        {
            AnalogInputSampleRate = robotModel.AnalogInputSampleRate;
            foreach (var subsystem in robotModel.Subsystem)
            {
                /*foreach (var analogInput in subsystem.AnalogInput)
                {
                    AddAnalogInput(analogInput);
                }*/
                foreach (var canTalon in subsystem.CANTalons)
                {
                    AddCANTalon(canTalon);
                }
                /*foreach (var digitalInput in subsystem.DigitalInput)
                {
                    AddDigitalInput(digitalInput);
                }
                foreach (var pwmOutput in subsystem.PWMOutput)
                {
                    AddPWMOutput(pwmOutput);
                }
                foreach (var relayOutput in subsystem.RelayOutput)
                {
                    AddRelayOutput(relayOutput);
                }
                foreach (var quadEncoder in subsystem.QuadEncoder)
                {
                    AddQuadEncoder(quadEncoder);
                }*/
            }
            foreach (var joystick in robotModel.Joystick)
            {
                AddJoystick(joystick);
            }
        }
        #endregion

        #region Private Fields
        private Dictionary<int, CANTalonIOService> _canTalonIOServices = new Dictionary<int, CANTalonIOService>();
        #endregion
    }
}
