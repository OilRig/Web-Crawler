using Crawler.Logging.Intefaces;
using NLog;
using System;

namespace Crawler.Logging.Loggers
{
    public class NlogLogger : IAppLogger
    {

        public const string LoggerName = "NLog";
        public Logger CurrentLogger { get; }
        public NlogLogger()
        {
            CurrentLogger = LogManager.GetLogger(LoggerName);
        }

        public void LogError(Exception exception, string info)
        {
            CurrentLogger.Error(exception, "Error: {0}! \n Exception: {1}", info);
        }

    }
}
