using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks;
namespace Logging
{
    public class LoggerFactory
    {
        public static ILogger ConfigureLogger<T>()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Debug()
                .CreateLogger()
                .ForContext<T>();

            Log.Logger = logger; // Set the global logger instance

            return logger;
        }
    }
}
