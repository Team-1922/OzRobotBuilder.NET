using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.FakeIOService
{
    public class FakeJoystickIOService : IJoystickIOService
    {
        public FakeJoystickIOService(Joystick joystick)
        {
            ID = joystick.ID;
            foreach (var axis in joystick.Axis)
            {
                _axes.Add(axis.ID, axis.Value);
            }
            foreach (var button in joystick.Button)
            {
                _buttons.Add(button.ID, button.Value);
            }
        }
        #region IJoystickIOService
        public IReadOnlyDictionary<int, double> Axes
        {
            get
            {
                return _axes;
            }
        }

        public IReadOnlyDictionary<int, bool> Buttons
        {
            get
            {
                return _buttons;
            }
        }

        public int ID { get; private set; }
        #endregion

        public void SetJoystickButtonsAxes(Joystick joystick)
        {
        }

        #region Private Fields
        Dictionary<int, double> _axes = new Dictionary<int, double>();
        Dictionary<int, bool> _buttons = new Dictionary<int, bool>();
        #endregion
    }
}