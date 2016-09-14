using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.Logging
{
    /// <summary>
    /// A write-only stream which delegates the streaming to multiple endpoints
    /// </summary>
    public class MultiStream : Stream
    {
        public void AddStream(Stream stream)
        {
            _streams.Add(stream);
        }

        #region Stream
        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public override long Position
        {
            get
            {
                return Length - 1;
            }

            set
            {
                throw new InvalidOperationException();
            }
        }

        public override void Flush()
        {
            foreach(var stream in _streams)
            {
                stream.Flush();
            }
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new InvalidOperationException();
        }
        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            foreach (var stream in _streams)
            {
                stream.Write(buffer, offset, count);
            }
        }
        #endregion

        /// <summary>
        /// These methods can be optimized for this class, becuase we can do the operation to all of the streams simultaniously
        /// </summary>
        #region Async Methods
        public new async Task FlushAsync()
        {
            List<Task> flushTasks = new List<Task>();
            foreach(var stream in _streams)
            {
                flushTasks.Add(stream.FlushAsync());
            }
            foreach(var flushTask in flushTasks)
            {
                await flushTask;
            }
        }
        public new async Task WriteAsync(byte[] buffer, int offset, int count)
        {
            List<Task> writeTasks = new List<Task>();
            foreach (var stream in _streams)
            {
                writeTasks.Add(stream.WriteAsync(buffer, offset, count));
            }
            foreach (var writeTask in writeTasks)
            {
                await writeTask;
            }
        }
        #endregion

        #region Private Fields
        private List<Stream> _streams = new List<Stream>();
        #endregion
    }
}
