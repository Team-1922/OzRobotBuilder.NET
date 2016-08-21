using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// The base class for all IO services
    /// </summary>
    public interface IIOService
    {
        /// <summary>
        /// The ID of this service
        /// </summary>
        int ID { get; }
    }
}
