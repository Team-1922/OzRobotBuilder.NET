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
        public static async Task<HeaderContent> SocketReadHeader(NetworkStream stream)
        {
            byte[] headerBytes = new byte[HeaderContent.HeaderSize];
            await stream.ReadAsync(headerBytes, 0, headerBytes.Length);

            return HeaderContent.FromBytes(headerBytes);
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
            return (await SocketReceiveAsync(stream)).ToResponse();
        }
        public static async Task<Request> SocketReceiveRequestAsync(NetworkStream stream)
        {
            return (await SocketReceiveAsync(stream)).ToRequest();
        }
        public static async Task SocketSendAsync(NetworkStream stream, SocketMessage message)
        {
            if (stream == null)
                throw new ArgumentNullException("stream", "Network Stream was Null!");
            if (message == null)
                throw new ArgumentNullException("message", "Message Was Null");

            var writeBuffer = message.ToBytes();
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

        public static string DecodeString(byte[] str, int index, int count)
        {
            return Encoding.UTF8.GetString(str, index, count);
        }
        public static string DecodeString(byte[] str)
        {
            return Encoding.UTF8.GetString(str);
        }
        public static byte[] EncodeString(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}
