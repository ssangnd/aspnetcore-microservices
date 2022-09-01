using Serilog;
using Microsoft.Extensions.Hosting;

namespace Common.Logging;


public static class Serilogger
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, configuration) =>
     {
         var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");
         var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

         configuration
         .WriteTo.Debug()
         .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
         .Enrich.FromLogContext()
         .Enrich.WithProperty("Environment", environmentName)
         .Enrich.WithProperty("Application", applicationName)
         .ReadFrom.Configuration(context.Configuration);
     };
}