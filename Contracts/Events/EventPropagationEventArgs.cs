using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts.Events
{
    public class EventPropagationEventArgs
    {
        public EventPropagationEventArgs(Protocall.Method method, string propertyName, string propertyValue)
        {
            Method = method;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
        public Protocall.Method Method { get; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; }
    }
}
