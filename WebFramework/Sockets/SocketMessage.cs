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
        public SocketMessage(string content)
        {
            _content = content;
            _headerContent = new HeaderContent(_content.Length);
        }
        public SocketMessage(object obj) : this(obj.ToString())
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
            return new Request(Content);
        }
        public Response ToResponse()
        {
            return new Response(Content);
        }


        #region Private Fields
        public string _content = "";
        private HeaderContent _headerContent;
        #endregion
    }
}
