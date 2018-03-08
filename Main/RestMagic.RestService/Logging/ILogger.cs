using System;

namespace RestMagic.RestService.Logging
{
    public interface ILogger
    {
        void LogError( Exception ex);
        void LogTrace(string message);
        void Flush();
    }
}