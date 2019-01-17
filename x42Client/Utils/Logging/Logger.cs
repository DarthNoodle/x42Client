using System;


namespace x42Client.Utils.Logging
{
    /// <summary>
    /// this is a wrapper to encapsulate the LoggingFactory & LogProvider and provides
    /// the main entry point for logging within the application
    /// </summary>
    //todo: implement minimum log severity
    public class Logger
    {
        private static ILoggerFactory _Factory = null;

        private static LoggerType _LogType = LoggerType.CONSOLE;


        private static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_Factory == null)
                {
                    _Factory = new LoggerFactory();
                }
                return _Factory;
            }
            set { _Factory = value; }
        }

        public static void SetLogType(LoggerType type) => _LogType = type;

        public static void Debug(string message) => LoggerFactory.Get(_LogType).Debug(message);

        public static void Debug(string message, Exception exception) => LoggerFactory.Get(_LogType).Debug(message, exception);

        public static void Error(string message) => LoggerFactory.Get(_LogType).Error(message);

        public static void Error(string message, Exception exception) => LoggerFactory.Get(_LogType).Error(message, exception);

        public static void Fatal(string message) => LoggerFactory.Get(_LogType).Fatal(message);

        public static void Fatal(string message, Exception exception) => LoggerFactory.Get(_LogType).Fatal(message, exception);

        public static void Info(string message) => LoggerFactory.Get(_LogType).Info(message);

        public static void Info(string message, Exception exception) => LoggerFactory.Get(_LogType).Info(message, exception);

        public static void Trace(string message) => LoggerFactory.Get(_LogType).Trace(message);

        public static void Trace(string message, Exception exception) => LoggerFactory.Get(_LogType).Trace(message, exception);

        public static void Warn(string message) => LoggerFactory.Get(_LogType).Warn(message);

        public static void Warn(string message, Exception exception) => LoggerFactory.Get(_LogType).Warn(message, exception);
    }
}
