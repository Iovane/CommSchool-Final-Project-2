using CommSchool_Final_Project_2.Domain;
using CommSchool_Final_Project_2.Interfaces;
using CommSchool_Final_Project_2.Middlewares;
using CommSchool_Final_Project_2.Services;
using CommSchool_Final_Project_2.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Homework_19.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace CommSchool_Final_Project_2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Graylog(new GraylogSinkOptions
            {
                HostnameOrAddress = "localhost",
                Port = 12201,
                TransportType = TransportType.Udp
            })
            .CreateLogger();

        builder.Host.UseSerilog();
        
        var configuration = builder.Configuration;
        var appSettingsSection = builder.Configuration.GetSection("AppSettings");
        var appSettings = appSettingsSection.Get<AppSettings>();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<Program>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}