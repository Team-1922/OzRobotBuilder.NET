using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.FakeIOService
{
    public class FakeRelayOutputIOService : IRelayOutputIOService
    {
        #region IRelayOutputIOService
        public bool ValueAsBool { get; set; }
        public bool ReverseValueAsBool { get; set; }
        public double Value
        {
            get
            {
                return ValueAsBool ? 1.0 : 0;
            }

            set
            {
                ValueAsBool = value > 0.0;
            }
        }
        public int ID { get; internal set; }
        #endregion
    }
}
