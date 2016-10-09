using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// Responds to requests on the data
    /// </summary>
    /// <remarks>
    /// While this does not use any network-specific code, it is still put in the WebFramework assembly becuase it is the
    /// best place to put it for now
    /// </remarks>
    public interface IRequestDelegator
    {        
        /// <summary>
        /// Processes a given request and returns the response
        /// </summary>
        /// <param name="request">the request</param>
        /// <returns>a basic resopnse with status code and body</returns>
        Task<Response> ProcessRequestAsync(Request request);
        /// <summary>
        /// The data this request delegator acts upon
        /// </summary>
        IHierarchialAccessRoot Data { get; }
    }
}
