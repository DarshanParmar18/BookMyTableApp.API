using BookMyTableApp.Data;
using BookMyTableApp.Service;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

namespace BookMyTableApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Create a builder for the web application
                var builder = WebApplication.CreateBuilder(args);

            // Add Serilog configuration
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            // Use Serilog for logging
            builder.Host.UseSerilog();

            // Load configuration from appsettings.Development.json if in development
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            }

            // Add services to the container
            builder.AddServiceDefaults();


            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddScoped<IRestaurantService, RestaurantService>();


            builder.Services.AddDbContext<BookMyTableDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext") ?? "")
                .EnableSensitiveDataLogging() // <-- Do not use this in production, only for dev purpose
            );

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Exception handaling. Create a middleware and include that here
            // Enable Serilog exception logging
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    Log.Error(exception, "Unhandled exception occured. {ExceptionDetails}", exception?.ToString());

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync("An unexpected error occured. Please try again later.");
                });
            });

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.MapDefaultEndpoints();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
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









































/*using BookMyTableApp.Data;
using BookMyTableApp.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Debugging;

namespace BookMyTableApp.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        var configuration = builder.Configuration;
        // Add services to the container.

        builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        builder.Services.AddScoped<IRestaurantService, RestaurantService>();

        builder.Services.AddDbContext<BookMyTableDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DbContext") ?? "")
        .EnableSensitiveDataLogging()  //<--Do not use this in production, only for dev purpose
        );


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
*/