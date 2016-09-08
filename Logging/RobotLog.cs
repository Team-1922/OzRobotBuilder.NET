using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Team1922.Logging
{
    public enum LogSeverity
    {
        /// <summary>
        /// Results in entire system shutdown
        /// </summary>
        Critical, 
        /// <summary>
        /// If an error occured, but the system could recover from it
        /// </summary>
        Error,
        /// <summary>
        /// If something did not go as well as it could have gone (usually a recommendation to do something else)
        /// </summary>
        Warning,
        /// <summary>
        /// If Nothing went wrong and a significant event occured
        /// </summary>
        Informational
    }

    /// <summary>
    /// A log entry data structure for logging all messages
    /// </summary>
    /// <remarks>
    /// Debugging runtime issues can sometimes be a pain, however getting the exact line this log call was made from, we can
    /// more easily see where the issues are occuring and at what time.  Also, this class is used for all log types, becuase 
    /// using the <see cref="System.Runtime.CompilerServices"/> does not add any runtime overhead
    /// </remarks>
    public class LogEntryData
    {
        /// <summary>
        /// Creates a new log entry with the given log parameters
        /// </summary>
        /// <param name="message">the message being logged</param>
        /// <param name="severity">the severity of this entry</param>
        /// <param name="callerName">the name of the caller of this method</param>
        /// <param name="filePath">the name of the file this method is being called from</param>
        /// <param name="lineNumber">the line number of the aforementioned file this method is being called from</param>
        public LogEntryData(string message, LogSeverity severity, [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            TimeStamp = DateTime.UtcNow;

            //only initialize caller information if this is fairly severe
            if (severity < LogSeverity.Warning)
            {
                CallerMemberName = callerName;
                CallerFilePath = filePath;
                LineNumber = lineNumber;
            }
            Severity = severity;
            Message = message;
        }
        /// <summary>
        /// The timestamp of this log
        /// </summary>
        public DateTime TimeStamp { get; }
        /// <summary>
        /// The Method where this entry was created
        /// </summary>
        public string CallerMemberName { get; }
        /// <summary>
        /// The File where this entry was created
        /// </summary>
        public string CallerFilePath { get; }
        /// <summary>
        /// The Line Number where this entry was created
        /// </summary>
        public int LineNumber { get; }
        /// <summary>
        /// The severity of this entry
        /// </summary>
        public LogSeverity Severity { get; }
        /// <summary>
        /// The actual message of this entry
        /// </summary>
        public string Message { get; }

        public override string ToString()
        {
            //only put in a caller information of the entry if it is a fairly sever entry
            var header = Severity < LogSeverity.Warning ? $"at {CallerFilePath} in Method {CallerMemberName} on Line {LineNumber}\n" : " ";
            return $"[{Severity.ToString()}][{TimeStamp.ToString("u")}] {header}{Message}";
        }
    }

    public class RobotLog
    {
        #region Singleton Implementation
        public static RobotLog Instance
        {
            get
            {
                if(null == _instance)
                {
                    _instance = new RobotLog();
                }
                return _instance;
            }
        }
        private static RobotLog _instance;
        #endregion

        private RobotLog()
        {
            //TODO: add more than just this MemoryStream (i.e. file stream, network stream for OzRobotBulider.NET application, etc.)
            _stream.AddStream(new MemoryStream());

            _streamWriter = new StreamWriter(_stream);
        }
        
        public void CaughtException<T>(T exception, LogSeverity severity, bool rethrow, [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) where T : Exception
        {
            WriteLine(new LogEntryData($"{exception.ToString()}", severity, callerName, filePath, lineNumber)).Wait();
            if (rethrow)
                throw exception;
        }

        /// <summary>
        /// Writes the given log data to the streams
        /// </summary>
        /// <param name="logData">the data to log</param>
        /// <returns></returns>
        public async Task WriteLine(LogEntryData logData)
        {
            await _streamWriter.WriteAsync(logData.ToString());
        }

        /// <summary>
        /// Writes a log with the given log data as parameters
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severity"></param>
        /// <param name="callerName"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public async Task WriteLine(string message, LogSeverity severity, [CallerMemberName] string callerName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            await WriteLine(new LogEntryData(message, severity, callerName, filePath, lineNumber));
        }

        #region Private Fields
        private MultiStream _stream = new MultiStream();
        private StreamWriter _streamWriter;
        #endregion
    }
}
