using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class SocketMessage
    {
        public SocketMessage(string socketPath, Request request)
        {
            Body = request.Text;
            Header = new HeaderContent(socketPath, Body.Length);
        }
        public SocketMessage(string socketPath, Response response)
        {
            Body = response.Text;
            Header = new HeaderContent(socketPath, Body.Length);
        }
        public SocketMessage(byte[] message)
        {
            //get the header bytes
            var header = new byte[HeaderContent.HeaderSize];
            Array.Copy(message, header, header.Length);

            //parse the bytes
            Header = new HeaderContent(header);

            //get the body bytes
            var _body = new byte[message.Length - header.Length];
            Array.Copy(message, header.Length, _body, 0, _body.Length);

            //parse the body bytes
            Body = Encoding.UTF8.GetString(_body);
        }
        public SocketMessage(HeaderContent header, byte[] body)
        {
            Header = header;
            Body = Encoding.UTF8.GetString(body);
        }

        public HeaderContent Header { get; }
        public string Body { get; }

        public byte[] Bytes
        {
            get
            {
                var bytes = new byte[HeaderContent.HeaderSize + Header.ContentSize];

                //copy the header bytes
                Array.Copy(Header.HeaderBytes, 0, bytes, 0, HeaderContent.HeaderSize);

                //copy the body bytes
                var bodyBytes = Encoding.UTF8.GetBytes(Body);
                Array.Copy(bodyBytes, 0, bytes, HeaderContent.HeaderSize, bodyBytes.Length);

                return bytes;
            }
        }
    }
}
