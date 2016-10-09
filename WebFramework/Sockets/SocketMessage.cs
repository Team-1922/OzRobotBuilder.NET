using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// A low level socket message class
    /// </summary>
    public class SocketMessage
    {
        /// <summary>
        /// Initialize from an incoming socket message
        /// </summary>
        /// <param name="header">the header of the message</param>
        /// <param name="bodyBytes">the body bytes of the message</param>
        public SocketMessage(HeaderContent header, byte[] bodyBytes)
        {
            _headerContent = header;
            _content = Utils.DecodeString(bodyBytes);
        }
        /// <summary>
        /// Initialize an outgoing socket message
        /// </summary>
        /// <param name="content">the string message to send</param>
        public SocketMessage(string content)
        {
            _content = content;
            _headerContent = new HeaderContent(_content.Length);
        }
        /// <summary>
        /// Initialize an outgoing socket message
        /// </summary>
        /// <param name="request">the request to initialize from</param>
        public SocketMessage(Request request) : this(request.ToString())
        {
        }
        /// <summary>
        /// Initialize an outgoing socket message
        /// </summary>
        /// <param name="response">the request to initialize from</param>
        public SocketMessage(Response response) : this(response.ToString())
        {
        }

        /// <summary>
        /// The header information of this message
        /// </summary>
        public HeaderContent Header
        {
            get
            {
                return _headerContent;
            }
        }
        /// <summary>
        /// The string representation of this message
        /// </summary>
        public string Content
        {
            get
            {
                return _content;
            }
        }

        /// <summary>
        /// Converts this message to a series of bytes
        /// </summary>
        /// <returns>a series of bytes representing this rmessage</returns>
        public byte[] ToBytes()
        {
            var bytes = new byte[HeaderContent.HeaderSize + Header.ContentSize];

            //copy the header bytes
            Array.Copy(Header.HeaderBytes, 0, bytes, 0, HeaderContent.HeaderSize);

            //copy the body bytes
            var bodyBytes = Utils.EncodeString(_content);
            Array.Copy(bodyBytes, 0, bytes, HeaderContent.HeaderSize, bodyBytes.Length);

            return bytes;
        }
        /// <summary>
        /// Converts this message to a request
        /// </summary>
        /// <returns>the request representation of this message</returns>
        public Request ToRequest()
        {
            return new Request(Content);
        }
        /// <summary>
        /// Convers this message to a response
        /// </summary>
        /// <returns>the resopnse representation of this message</returns>
        public Response ToResponse()
        {
            return new Response(Content);
        }


        #region Private Fields
        private string _content = "";
        private HeaderContent _headerContent;
        #endregion
    }
}
