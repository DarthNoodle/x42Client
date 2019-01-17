namespace x42Client.Utils.Logging
{
    public interface ILoggerFactory
    {
        ILogProvider Get();
        ILogProvider Get(LoggerType type);
    }
}
