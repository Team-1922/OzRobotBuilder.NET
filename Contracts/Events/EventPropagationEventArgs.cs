using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts.Events
{
    public class EventPropagationEventArgs
    {
        public EventPropagationEventArgs(string httpMethod, string propertyName, string propertyValue)
        {
            HTTPMethod = httpMethod;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
        public string HTTPMethod { get; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; }
    }
}
