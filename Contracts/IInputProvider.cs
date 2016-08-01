using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// This interface is used by the providers which need robot input;
    /// </summary>
    /// <remarks>
    /// Output values can be updated to the IO service when they are set, however
    /// input values must be updated every update cycle in order for event handlers
    /// to properly recognize a state change. 
    /// 
    /// Typically all interface properties with only a get accessor will do this update
    /// </remarks>
    public interface IInputProvider
    {
        /// <summary>
        /// Called every update cycle to update input values from the <see cref="IRobotIOService"/>
        /// </summary>
        void UpdateInputValues();
    }
}
