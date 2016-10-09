using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// Represents a response from the web
    /// </summary>
    public class Response
    {
        /// <summary>
        /// creates an empty response
        /// </summary>
        public Response() { }
        /// <summary>
        /// Deserializes a response from a string
        /// </summary>
        /// <param name="content">the content of a web message to deserialize</param>
        public Response(string content)
        {
            var split = Utils.SplitString(content, 2);

            int statusCode;
            if (!int.TryParse(split[0], out statusCode))
            {
                throw new ArgumentException("content", "Invalid HttpStatusCode");
            }
            StatusCode = (System.Net.HttpStatusCode)statusCode;

            Body = split[1];
        }

        /// <summary>
        /// The <see cref="HttpStatusCode"/> of the response
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// The body of the response
        /// </summary>
        /// <remarks>
        /// This will include any relevent error information, such as exception messages
        /// </remarks>
        public string Body { get; set; } = "";

        /// <summary>
        /// The string representation of this instance
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{(int)StatusCode} {Body}";
        }
    }
}
