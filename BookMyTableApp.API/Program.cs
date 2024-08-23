using BookMyTableApp.Data;
using BookMyTableApp.Service;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Diagnostics;
//
using Microsoft.IdentityModel.Logging;
using System.Text.Json.Serialization;
//
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

namespace BookMyTableApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog With the settings
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddApplicationInsightsTelemetry();

                var configuration = builder.Configuration;

                builder.AddServiceDefaults();

                builder.Host.UseSerilog((context, services, loggerConfiguration) =>
                {
                    var telemetryConfig = services.GetRequiredService<TelemetryConfiguration>();
                    loggerConfiguration
                        .ReadFrom.Configuration(context.Configuration)
                        .WriteTo.ApplicationInsights(telemetryConfig, TelemetryConverter.Events);
                });

                Log.Information("Starting the application...");


                // Add services to the container.


                builder.Services.AddControllers();


                builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
                .WriteTo.ApplicationInsights(
                  services.GetRequiredService<TelemetryConfiguration>(),
                  TelemetryConverter.Events));


                builder.Services.AddDbContext<BookMyTableDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbContext") ?? "")
                .EnableSensitiveDataLogging()  //<--Do not use this in production, only for dev purpose
                );

                //In production, modify this with the actual domains you want to allow
                builder.Services.AddCors(o => o.AddPolicy("default", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
                builder.Services.AddScoped<IRestaurantService, RestaurantService>();

                var app = builder.Build();

                //Exception hanlding. Create a middleware and include that here
                // Enable Serilog exception logging
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature?.Error;

                        Log.Error(exception, "Unhandled exception occurred. {ExceptionDetails}", exception?.ToString());
                        Console.WriteLine(exception?.ToString());
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
                    });
                });

                app.UseMiddleware<RequestResponseLoggingMiddleware>();


                app.MapDefaultEndpoints();


                app.UseCors("default");
                // Configure the HTTP request pipeline.
                // if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}