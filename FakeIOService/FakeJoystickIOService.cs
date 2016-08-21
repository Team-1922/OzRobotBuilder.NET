using System;
using System.Collections.Generic;
using Team1922.MVVM.Contracts;

namespace Team1922.FakeIOService
{
    public class FakeJoystickIOService : IJoystickIOService
    {
        #region IJoystickIOService
        public IReadOnlyDictionary<uint, double> Axes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyDictionary<uint, bool> Buttons
        {
            get
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
        }
        #endregion
    }
}