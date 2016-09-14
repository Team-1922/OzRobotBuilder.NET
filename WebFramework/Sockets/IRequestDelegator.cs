using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public interface IRequestDelegator
    {        
        /// <summary>
        /// Processes a given request and returns the response
        /// </summary>
        /// <param name="method">the request</param>
        /// <returns>a basic resopnse with status code and body</returns>
        Task<Response> ProcessRequestAsync(Request request);
        IHierarchialAccessRoot Data { get; }
    }
}
