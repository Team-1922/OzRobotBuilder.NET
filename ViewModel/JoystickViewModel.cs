using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.MVVM.ViewModels
{
    public class JoystickViewModel : IJoystickProvider
    {
        public IEnumerable<IJoystickAxisProvider> Axes
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

        public IEnumerable<IJoystickButtonProvider> Buttons
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

        public int ID
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

        public string Name
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

        public void SetJoystick(Joystick joystick)
        {
            throw new NotImplementedException();
        }

        public void UpdateInputValues()
        {
            throw new NotImplementedException();
        }
    }
}