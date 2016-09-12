using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public static class Utils
    {
        /// <summary>
        /// Gets the length of the request from the header
        /// </summary>
        /// <param name="header">the first bytes of the header <see cref="HeaderBytes"/></param>
        /// <returns></returns>
        public static HeaderContent ParseHeader(byte[] header)
        {
            return new HeaderContent(header);
        }
        public static async Task<HeaderContent> SocketReadHeader(NetworkStream stream)
        {
            byte[] headerBytes = new byte[HeaderContent.HeaderSize];
            await stream.ReadAsync(headerBytes, 0, headerBytes.Length);

            return ParseHeader(headerBytes);
        }
        public static async Task<SocketMessage> SocketReceiveAsync(NetworkStream stream)
        {
            //get the header
            var header = await SocketReadHeader(stream);

            //get the body
            byte[] body = new byte[header.ContentSize];
            await stream.ReadAsync(body, 0, body.Length);

            return new SocketMessage(header, body);
        }

        public static async Task<Response> SocketReceiveResponseAsync(NetworkStream stream)
        {
            return ParseResponse((await SocketReceiveAsync(stream)).Body);
        }
        public static async Task<Request> SocketReceiveRequestAsync(NetworkStream stream)
        {
            return ParseRequest((await SocketReceiveAsync(stream)).Body);
        }
        public static async Task SocketSendAsync(NetworkStream stream, SocketMessage message)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Network Stream was Null!");
            if (message == null)
                throw new ArgumentNullException("message", "Message Was Null");

            var writeBuffer = message.Bytes;
            await stream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
            await stream.FlushAsync();
        }

        public static string[] SplitString(string text, int count)
        {
            var parts = text.Split(new char[] { ' ' }, count, StringSplitOptions.None).ToList();
            if (parts.Count < count)
            {
                while (parts.Count < count)
                    parts.Add("");
            }
            return parts.ToArray();
        }

        public static Response ParseResponse(string text)
        {
            var parts = SplitString(text, 2);

            Response response = new Response();

            int statusCode;
            if (int.TryParse(parts[0], out statusCode))
            {
                response.StatusCode = (HttpStatusCode)statusCode;
            }
            else
            {
                throw new Exception("Invalid Status Code in Response");
            }

            response.Body = parts[1];
            return response;
        }
        public static Request ParseRequest(string text)
        {
            var parts = SplitString(text, 3);

            Request request = new Request();

            request.Method = Protocall.StringToMethod(parts[0]);
            request.Path = parts[1];
            request.Body = parts[2];
            return request;
        }

        public static string SerializeResponse(Response response)
        {
            return $"{(int)response.StatusCode} {response.Body}";
        }
        public static string SerializeRequest(Request request)
        {
            return $"{request.Method} {request.Path} {request.Body}";
        }

        /// <summary>
        /// This makes a socket based on the given port using reasonable settings
        /// </summary>
        /// <returns>a new socket instance</returns>
        public static Socket MakeSocket()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.ReceiveTimeout = 5000; // receive timout 5 seconds
            socket.SendTimeout = 5000; // send timeout 5 seconds 
            return socket;
        }


    }
}
