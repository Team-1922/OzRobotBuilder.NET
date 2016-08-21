using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.FakeIOService
{
    public class FakePWMOutputIOService : IPWMOutputIOService
    {
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
        public int ID { get; internal set; }
        #endregion
    }
}
