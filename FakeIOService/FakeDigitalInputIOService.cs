using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;
using Team1922.MVVM.Models;

namespace Team1922.FakeIOService
{
    public class FakeDigitalInputIOService : IDigitalInputIOService
    {
        public FakeDigitalInputIOService(DigitalInput digitalInput)
        {
            ValueAsBool = digitalInput.Value;
            ID = digitalInput.ID;
        }
        #region IDigitalInputIOService
        public double Value
        {
            get
            {
                return ValueAsBool ? 1.0 : 0.0;
            }
        }
        public bool ValueAsBool { get; private set; }
        public int ID { get; private set; }
        #endregion
    }
}
