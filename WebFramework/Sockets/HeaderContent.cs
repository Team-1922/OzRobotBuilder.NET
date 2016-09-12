using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class HeaderContent
    {
        public static int HeaderSize { get { return SocketPathMaxSize + ContentSizeSize; } }
        public static int SocketPathMaxSize { get { return 12; } }
        public static int ContentSizeSize { get { return 4; } }

        public HeaderContent(string socketPath, int size)
        {
            if (socketPath.Length > SocketPathMaxSize)
                throw new Exception("Socket Path is Too Long");

            _socketPath = socketPath;
            _size = size;

            _content = new byte[HeaderSize];
            Encoding.UTF8.GetBytes(_socketPath).CopyTo(_content, 0);
            BitConverter.GetBytes(size).CopyTo(_content, 12);
        }

        public HeaderContent(byte[] bytes)
        {
            if (bytes.Length != HeaderSize)
                throw new ArgumentException($"Header Must Be Exactly {HeaderSize} Bytes!");
            _content = new byte[HeaderSize];
            bytes.CopyTo(_content, 0);

            _socketPath = Encoding.UTF8.GetString(bytes, 0, SocketPathMaxSize);
            _size = BitConverter.ToInt32(bytes, SocketPathMaxSize);
        }

        public string SocketPath
        {
            get
            {
                return _socketPath;
            }
        }
        public int ContentSize
        {
            get
            {
                return _size;
            }
        }
        public byte[] HeaderBytes
        {
            get
            {
                return _content;
            }
        }

        #region Private Fields
        private string _socketPath;
        private int _size;
        private byte[] _content;
        #endregion
    }
}
