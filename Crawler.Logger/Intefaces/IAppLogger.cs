using System;

namespace Crawler.Logging.Intefaces
{
    public interface IAppLogger
    {
        void LogError(Exception exception, string info);
    }
}
