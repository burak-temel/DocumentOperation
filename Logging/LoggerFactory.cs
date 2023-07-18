using Serilog;

namespace Logging
{
    public static class LoggerFactory
    {
        public static ILogger ConfigureLogger<T>()
        {
            var logDirectory = $"../Logging/logfiles";

            // Create the log directory if it doesn't exist
            Directory.CreateDirectory(logDirectory);

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"{logDirectory}/log.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Debug()
                .CreateLogger()
                .ForContext<T>();

            return logger;
        }
    }
}
