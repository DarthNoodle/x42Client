using System;


namespace x42Client.Utils.Logging
{
    public class ConsoleLogger : ILogProvider
    {
        public void Debug(string message) => LogPriv(message, LogSeverity.Debug);

        public void Debug(string message, Exception exception) => LogPriv(message, LogSeverity.Debug, exception);

        public void Error(string message) => LogPriv(message, LogSeverity.Error);

        public void Error(string message, Exception exception) => LogPriv(message, LogSeverity.Error, exception);

        public void Fatal(string message) => LogPriv(message, LogSeverity.Fatal);

        public void Fatal(string message, Exception exception) => LogPriv(message, LogSeverity.Fatal, exception);

        public void Info(string message) => LogPriv(message, LogSeverity.Informational);

        public void Info(string message, Exception exception) => LogPriv(message, LogSeverity.Informational, exception);

        public void Trace(string message) => LogPriv(message, LogSeverity.Trace);

        public void Trace(string message, Exception exception) => LogPriv(message, LogSeverity.Trace, exception);

        public void Warn(string message) => LogPriv(message, LogSeverity.Warning);

        public void Warn(string message, Exception exception) => LogPriv(message, LogSeverity.Warning, exception);

        private void LogPriv(string message, LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Trace:
                    Console.WriteLine($"[TRACE] - {DateTime.Now} - {message}");
                    break;
                case LogSeverity.Debug:
                    Console.WriteLine($"[DEBUG] - {DateTime.Now} - {message}");
                    break;
                case LogSeverity.Informational:
                    Console.WriteLine($"[INFO] - {DateTime.Now} - {message}");
                    break;
                case LogSeverity.Warning:
                    Console.WriteLine($"[WARN] - {DateTime.Now} - {message}");
                    break;
                case LogSeverity.Error:
                    Console.WriteLine($"[ERROR] - {DateTime.Now} - {message}");
                    break;
                case LogSeverity.Fatal:
                    Console.WriteLine($"[FATAL] - {DateTime.Now} - {message}");
                    break;
            }
        }

        private void LogPriv(string message, LogSeverity severity, Exception ex)
        {
            switch (severity)
            {
                case LogSeverity.Trace:
                    Console.WriteLine($"[TRACE] - {DateTime.Now} - {message}");
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"{ex.StackTrace}");
                    break;
                case LogSeverity.Debug:
                    Console.WriteLine($"[DEBUG] - {DateTime.Now} - {message}");
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"{ex.StackTrace}");
                    break;
                case LogSeverity.Informational:
                    Console.WriteLine($"[INFO] - {DateTime.Now} - {message}");
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"{ex.StackTrace}");
                    break;
                case LogSeverity.Warning:
                    Console.WriteLine($"[WARN] - {DateTime.Now} - {message}");
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"{ex.StackTrace}");
                    break;
                case LogSeverity.Error:
                    Console.WriteLine($"[ERROR] - {DateTime.Now} - {message}");
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"{ex.StackTrace}");
                    break;
                case LogSeverity.Fatal:
                    Console.WriteLine($"[FATAL] - {DateTime.Now} - {message}");
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"{ex.StackTrace}");
                    break;
            }
        }
    }
}
