using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts.Events
{
    /// <summary>
    /// The event arguments of a propagation event
    /// </summary>
    public class EventPropagationEventArgs
    {
        public EventPropagationEventArgs(Protocall.Method method, string propertyName, string propertyValue/*, string updateSource*/)
        {
            Method = method;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
        /// <summary>
        /// The kind of change that occured (get, set, delete, etc.)
        /// </summary>
        public Protocall.Method Method { get; }
        /// <summary>
        /// The name of the property changed (i.e. "RobotMap.Value1"
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// The new value of the property
        /// </summary>
        public string PropertyValue { get; }
        /// <summary>
        /// Where this update originated
        /// </summary>
        /// <remarks>
        /// This is primarily used for networking to prevent infinite update loops accross the network
        /// </remarks>
        //public string UpdateSource { get; }
    }
}
