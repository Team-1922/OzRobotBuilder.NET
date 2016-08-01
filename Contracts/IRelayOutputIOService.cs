using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.MVVM.Contracts
{
    /// <summary>
    /// This abstracts away the different relay modes and only stores values as booleans for forward and reverse
    /// NOTE: <see cref="IOutputService.ValueAsBool"/> is the Forward output
    /// </summary>
    public interface IRelayOutputIOService : IOutputService
    {
        /// <summary>
        /// The state of the reverse output
        /// </summary>
        bool ReverseValueAsBool { get; set; }
    }
}
