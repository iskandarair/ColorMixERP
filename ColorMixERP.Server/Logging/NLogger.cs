using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ColorMixERP.Server.Logging
{
    public class NLogger : ILogger
    {
        private readonly NLog.Logger _nLogger = NLog.LogManager.GetLogger("ColorMixERP");

        public NLogger()
        {

        }

        public void Error(Exception exception)
        {
            LogToFile($@"------------!!!E X C E P T I O N!!!------------{Environment.NewLine} {exception.Message}
                        {Environment.NewLine} {exception.StackTrace}", LogLevel.Error);
        }

        public void Error(string message)
        {
            LogToFile(message, LogLevel.Error);
        }

        public void Info(string message)
        {
            LogToFile(message, LogLevel.Info);
        }

        public void LogToFile(string message, LogLevel level)
        {
            var logEvent = new LogEventInfo(level, _nLogger.Name, message + Environment.NewLine);

            _nLogger.Log(logEvent);
        }
    }
}
