using System.Net;
using AutoMapper;
using JRovnyBlog.Api.Images;
using JRovnyBlog.Api.Posts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Npgsql.Logging;
using Serilog;

namespace JRovnyBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddDbContext<ApplicationDbContext>();
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IConnectionService, ConnectionService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<IImagesService, ImagesService>();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "dist";
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("https://localhost:5001").AllowAnyMethod().AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Trace, false, false);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseExceptionHandler(appError =>
                {
                    HandleGlobalException(env, appError, logger);
                });
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }

        private static void HandleGlobalException(
            IWebHostEnvironment env,
            IApplicationBuilder appError,
            ILogger<Startup> logger)
        {
            appError.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature.Error;

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                logger.LogError(exception.ToString());

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "An Error Occurred",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = env.IsDevelopment() ? exception.ToString() : "Error",
                    Instance = context.Request.Path
                };

                var problemDetailsJson = JsonConvert.SerializeObject(
                    problemDetails,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.Indented
                    });

                await context.Response.WriteAsync(problemDetailsJson).ConfigureAwait(false);
            });
        }
    }
}
