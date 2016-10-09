using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// Represents a request over the web
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Creates a new Request instance
        /// </summary>
        public Request() { }
        /// <summary>
        /// Creates a new Request Instance with the given text
        /// </summary>
        /// <param name="content">the text to deserialite this instance from</param>
        public Request(string content)
        {
            var split = Utils.SplitString(content, 3);

            Protocall.Method method;
            if (!Enum.TryParse(split[0], out method))
            {
                throw new ArgumentException("content", "Invalid Protocall.Method");
            }
            Method = method;

            Path = split[1];
            Body = split[2];
        }

        /// <summary>
        /// The method of this request
        /// </summary>
        /// <remarks>
        /// This is what should be done with the given path and body
        /// </remarks>
        public Protocall.Method Method { get; set; }
        /// <summary>
        /// The path of this request
        /// </summary>
        /// <remarks>
        /// the location of the value on which to execute <see cref="Method"/>
        /// </remarks>
        public string Path { get; set; } = "";
        /// <summary>
        /// The body of this request
        /// </summary>
        /// <remarks>
        /// Not required, but if used it is typically the new value to update at <see cref="Path"/>
        /// </remarks>
        public string Body { get; set; } = "";

        /// <summary>
        /// Returns a string representation of this object which can be used as parameters in a constructor overload of this class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Method} {Path} {Body}";
        }
    }
}
