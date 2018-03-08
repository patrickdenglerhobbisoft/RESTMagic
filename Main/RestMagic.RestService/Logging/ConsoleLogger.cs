using System;
using System.Diagnostics;

namespace RestMagic.RestService.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void LogError( Exception ex)
        {
            Debug.WriteLine("Error: {0}", ex);
        }

        public void LogTrace( string message)
        {
            Debug.WriteLine("Trace: {0}", message);
        }

        public void Flush()
        {
            // Noop
        }
    }
}