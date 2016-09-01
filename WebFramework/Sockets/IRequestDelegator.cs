using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public interface IRequestDelegator
    {        
        /// <summary>
        /// Processes a given http request and returns the response
        /// </summary>
        /// <param name="method">the http verb used</param>
        /// <param name="path">the uri path to the resource</param>
        /// <param name="body">the body of the request</param>
        /// <returns>a basic resopnse with status code and body</returns>
        Task<Response> ProcessRequestAsync(Protocall.Method method, string path, string body);
        /// <summary>
        /// The root of this delegator; this typically is in the form "/foo/bar" with a leading, but no trailing forward slash
        /// </summary>
        string PathRoot { get; }
    }
}
