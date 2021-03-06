﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class HeaderContent
    {
        public static int HeaderSize { get { return ContentSizeSize; } }
        public static int ContentSizeSize { get { return 4; } }

        public HeaderContent(int size)
        {
            _size = size;

            //_content = new byte[HeaderSize];
            //Encoding.UTF8.GetBytes(_socketPath).CopyTo(_content, 0);
            //BitConverter.GetBytes(size).CopyTo(_content, 12);
        }

        public static HeaderContent FromBytes(byte[] bytes)
        {
            if (bytes.Length < HeaderSize)
                throw new ArgumentException($"Header Must Be {HeaderSize} Bytes!");
            
            var size = BitConverter.ToInt32(bytes, 0);
            
            return new HeaderContent(size);
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
                var content = new byte[HeaderSize];
                BitConverter.GetBytes(ContentSize).CopyTo(content, 0);
                return content;
            }
        }

        #region Private Fields
        private int _size;
        #endregion
    }
}
