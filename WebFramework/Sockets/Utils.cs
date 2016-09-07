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
        public const int HeaderBytes = 4;
        public static byte[] AddHeader(string writeText)
        {
            byte[] textBuffer = Encoding.UTF8.GetBytes(writeText);
            byte[] writeBuffer = new byte[textBuffer.Length + HeaderBytes];
            BitConverter.GetBytes(writeText.Length).CopyTo(writeBuffer, 0);
            textBuffer.CopyTo(writeBuffer, HeaderBytes);
            return writeBuffer;
        }
        /// <summary>
        /// Gets the length of the request from the header
        /// </summary>
        /// <param name="header">the first bytes of the header <see cref="HeaderBytes"/></param>
        /// <returns></returns>
        public static int ParseHeader(byte[] header)
        {
            if (header.Length != HeaderBytes)
                throw new ArgumentException("header", "Header Must be Exactly 4 Bytes");
            return BitConverter.ToInt32(header, 0);
        }
        public static async Task<string> SocketReceiveAsync(NetworkStream stream)
        {
            //get the header
            byte[] header = new byte[4];
            await stream.ReadAsync(header, 0, 4);

            //get the body
            int bodyLength = ParseHeader(header);
            byte[] body = new byte[bodyLength];
            await stream.ReadAsync(body, 0, bodyLength);

            var str = Encoding.UTF8.GetString(body);
            return str;
        }

        public static async Task<Response> SocketReceiveResponseAsync(NetworkStream stream)
        {
            return ParseResponse(await SocketReceiveAsync(stream));
        }
        public static async Task<Request> SocketReceiveRequestAsync(NetworkStream stream)
        {
            return ParseRequest(await SocketReceiveAsync(stream));
        }
        public static async Task SocketSendAsync(NetworkStream stream, Response response)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Network Stream was Null!");
            if (response == null)
                throw new ArgumentNullException("response", "Response Was Null");
            if (null == response.Body)
                throw new ArgumentNullException("response.Body", "Response Body was Null");

            var writeBuffer = AddHeader(response.Text);
            await stream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
            await stream.FlushAsync();
        }
        public static async Task SocketSendAsync(NetworkStream stream, Request request)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Network Stream was Null!");
            if (request == null)
                throw new ArgumentNullException("request", "Request Was Null");
            if (request.Body == null)
                throw new ArgumentNullException("requset.Body", "Request Body was Null");
            if (request.Path == null)
                throw new ArgumentNullException("request.Body", "Request Path was Null");

            var writeBuffer = AddHeader(request.Text);

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
