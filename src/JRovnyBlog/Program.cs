using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace JRovnyBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Building and running web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Failed to start web host");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, logger) =>
                {
                    logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
                    logger.Enrich.FromLogContext();
                    //if (context.HostingEnvironment.IsDevelopment())
                        logger.WriteTo.Console();
                    logger.WriteTo.File(new JsonFormatter(), @".\log\log-.txt",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: null,
                        rollOnFileSizeLimit: true);
                    Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
