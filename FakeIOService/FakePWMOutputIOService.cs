using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.FakeIOService
{
    public class FakePWMOutputIOService : IPWMOutputIOService
    {
        public FakePWMOutputIOService(PWMOutput pwmOutput)
        {
            Value = pwmOutput.Value;
            ID = pwmOutput.ID;
        }

        #region IPWMOutputIOService
        public double Value { get; set; }
        public bool ValueAsBool
        {
            get
            {
                return Math.Abs(Value) > 0;
            }

            set
            {
                Value = value ? 1.0 : 0.0;
            }
        }
        public int ID { get; private set; }
        #endregion
    }
}
