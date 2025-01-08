using ApiEndPoint.Logging.Enricher;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Utility;

namespace Endpoint.Extentions
{
    public static class SerilogExtention
    {
        public static IHostBuilder AddSerilog(this IHostBuilder host, string projectName = "", string seqServer = "http://localhost:5341")
        {
            if (string.IsNullOrEmpty(projectName))
            {
                projectName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            }
            host.UseSerilog((context, config) =>
             {
                 config
                 .WriteTo.Console(new RenderedCompactJsonFormatter())
                 .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger") || p.Value.ToString().Contains("health")))
                 .Enrich.WithProperty("Project", projectName)
                 .Enrich.WithClientIp()
                 //.Enrich.WithClientAgent()
                 .Enrich.WithExceptionDetails()
                 .Enrich.WithMemoryUsage()
                 .Enrich.WithProcessId()
                 .Enrich.WithProcessName()
                 .Enrich.WithThreadId()
                 .Enrich.WithThreadName()
                 .Enrich.WithSpan()
                 .Enrich.WithCorrelationId()
                 .Enrich.WithProperty("Version", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version)
                 .Enrich.With<UserEnricher>()
                 .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                 .Enrich.WithProperty("MachineName", Environment.GetEnvironmentVariable("COMPUTERNAME"))
                 .Enrich.WithProperty("MachineIpAddress", Utils.GetLocalIpAddress())
                 .Enrich.FromLogContext()
                 .WriteTo.Seq(seqServer);
             });

            return host;
        }

    }
}
