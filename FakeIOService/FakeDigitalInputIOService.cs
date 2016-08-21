using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.FakeIOService
{
    public class FakeDigitalInputIOService : IDigitalInputIOService
    {
        #region IDigitalInputIOService
        public double Value
        {
            get
            {
                return ValueAsBool ? 1.0 : 0.0;
            }
        }
        public bool ValueAsBool { get; internal set; }
        public int ID { get; internal set; }
        #endregion
    }
}
