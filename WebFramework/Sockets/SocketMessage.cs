using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class SocketMessage
    {
        public SocketMessage(HeaderContent header, byte[] bodyBytes)
        {
            _headerContent = header;
            _content = Utils.DecodeString(bodyBytes);
        }
        public SocketMessage(string socketPath, string content)
        {
            _content = content;
            _headerContent = new HeaderContent(socketPath, _content.Length);
        }
        public SocketMessage(string socketPath, Request request) : this(socketPath, request.Body)
        {
        }
        public SocketMessage(string socketPath, Response response) : this(socketPath, response.Body)
        {
        }

        public HeaderContent Header
        {
            get
            {
                return _headerContent;
            }
        }
        public string Content
        {
            get
            {
                return _content;
            }
        }

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
        public Request ToRequest()
        {
            Request ret = new Request();
            var split = Utils.SplitString(Content, 3);

            Protocall.Method method;
            if(!Enum.TryParse(split[0], out method))
            {
                return null;//TODO: exception?
            }
            ret.Method = method;

            ret.Path = split[1];
            ret.Body = split[2];
            return ret;
        }
        public Response ToResponse()
        {
            Response ret = new Response();
            var split = Utils.SplitString(Content, 2);

            int statusCode;
            if(!int.TryParse(split[0], out statusCode))
            {
                return null;
            }
            ret.StatusCode = (System.Net.HttpStatusCode)statusCode;

            ret.Body = split[1];
            return ret;
        }


        #region Private Fields
        public string _content = "";
        private HeaderContent _headerContent;
        #endregion
    }
}
