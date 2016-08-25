using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework
{
    /// <summary>
    /// The interface for a class which handles HTTP requests
    /// </summary>
    /// <remarks>
    /// Note the use of no Asp.NET classes here; this is because this set of classes should be used in both .NET core applications and .NET 4.6.1
    /// </remarks>
    public interface IRequestDelegator
    {
        /// <summary>
        /// Processes a given http request and returns the response
        /// </summary>
        /// <param name="method">the http verb used</param>
        /// <param name="path">the uri path to the resource</param>
        /// <param name="body">the body of the request</param>
        /// <returns>a basic resopnse with status code and body</returns>
        Task<BasicHttpResponse> ProcessRequestAsync(string method, string path, string body);
        /// <summary>
        /// The root of this delegator; this typically is in the form "/foo/bar" with a leading, but no trailing forward slash
        /// </summary>
        string PathRoot { get; }
    }
}
