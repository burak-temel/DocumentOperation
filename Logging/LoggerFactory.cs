using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks;
namespace Logging
{
    public class LoggerFactory
    {
        public static ILogger ConfigureLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Debug()
                .CreateLogger();

            return logger;
        }
    }
}
