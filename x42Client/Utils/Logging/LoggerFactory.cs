
namespace x42Client.Utils.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogProvider Get()
        {
            return new ConsoleLogger();
        }

        public ILogProvider Get(LoggerType type)
        {
            switch (type)
            {
                case LoggerType.CONSOLE: return new ConsoleLogger();
                default: return new ConsoleLogger();
            }//end of switch
        }
    }
}
