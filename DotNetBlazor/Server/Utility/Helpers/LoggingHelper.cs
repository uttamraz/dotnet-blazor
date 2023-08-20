using Serilog.Sinks.Elasticsearch;
using Serilog;
using Masking.Serilog;

namespace DotNetBlazor.Server.Utility.Helpers
{
    public static class LoggingHelper
    {
        private static readonly string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new Exception("ASPNETCORE_ENVIRONMENT is not configured");
        public static void InitLog(this WebApplicationBuilder builder)
        {
            List<string> maskingProp = new()
            {
                "Password", "Token", "password", "token"
            };
            Log.Logger = new LoggerConfiguration()
                 .Destructure.ByMaskingProperties(opts =>
                 {
                     foreach (string mask in maskingProp)
                     {
                         opts.PropertyNames.Add(mask);
                     }
                     opts.Mask = "******";
                 })
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                //.WriteTo.Console(new ElasticsearchJsonFormatter())
                .WriteTo.Elasticsearch(ConfigureElasticSink(builder.Configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            CreateHost(builder.Configuration, builder.Host);
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            var url = configuration["ElasticUrl"]??throw new Exception("ElasticUrl is not configured");
            return new ElasticsearchSinkOptions(new Uri(url))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"log-{DateTime.UtcNow:yyyy-mm-dd}"
            };
        }

        private static void CreateHost(IConfigurationRoot configuration, ConfigureHostBuilder host)
        {
            try
            {
                host.UseSerilog();
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed to start {configuration["ServiceName"]}", ex);
                throw;
            }
        }
    }
}
