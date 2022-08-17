using AutoMapper;
using HealthChecks.UI.Client;
using LiberacaoCredito.Devedor.API.Extensions;
using LiberacaoCredito.Devedor.API.Middlewares;
using LiberacaoCredito.Devedor.API.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO.Compression;

namespace LiberacaoCredito.Devedor.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvcConfigurations();
            services.Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue; // <-- ! long.MaxValue
                options.MultipartBoundaryLengthLimit = int.MaxValue;
                options.MultipartHeadersCountLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddResponseCompression(x =>
            {
                x.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Startup));
            services.AddApplicationInsightsTelemetry();
            services.AddFluentConfigurations();
            services.AddHealthcheckConfigurations(Configuration);
            services.AddSwaggerConfigurations(WebHostEnvironment);
            services.ConfigureIoC(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<ApplicationInsightsSettings> options)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseResponseCompression();

            long maxAlowBodySize = long.Parse(Configuration["Documentos:MaxSizeMG"]) * 1024 * 1024;
            if (!env.IsDevelopment())
            {
                app.Use(async (context, next) =>
                {
                    context.Features.Get<IHttpMaxRequestBodySizeFeature>()
                        .MaxRequestBodySize = maxAlowBodySize;
                    await next.Invoke();
                });
            }

            if (PlatformServices.Default.Application.ApplicationName != "testhost")
            {
                app.UseHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }).UseHealthChecksUI(setup =>
                {
                    setup.UIPath = "/health-ui";
                });
            }

            var urls_origins = Configuration.GetSection("Origins").Get<string[]>();
            app.UseCors(builder => builder
             .SetIsOriginAllowedToAllowWildcardSubdomains()
               .WithOrigins(urls_origins)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
              );

            if (!env.IsProduction())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            //app.UseAuthorization();
            //app.UseAuthentication();
            //app.UseLogMiddleware();

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ErrorHandlerMiddleware(options, env).Invoke
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
