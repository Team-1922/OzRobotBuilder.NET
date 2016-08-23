using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Models
{
    public partial class RelayOutput : INamedModel
    {
        /// <summary>
        /// Gets the boolean state of the forward value
        /// </summary>
        /// <returns>the boolean state of the forward value</returns>
        public bool GetForwardValueBool()
        {
            if(Direction == RelayDirection.Both || Direction == RelayDirection.ForwardOnly)
            {
                if(Value == RelayValue.On || Value == RelayValue.Forward)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the boolean state of the reverse value
        /// </summary>
        /// <returns>the boolean state of the reverse value</returns>
        public bool GetReverseValueBool()
        {
            if (Direction == RelayDirection.Both || Direction == RelayDirection.ReverseOnly)
            {
                if (Value == RelayValue.On || Value == RelayValue.Reverse)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
