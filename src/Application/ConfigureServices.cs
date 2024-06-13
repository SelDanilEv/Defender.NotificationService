using System.Reflection;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.NotificationService.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<INotificationService, Services.NotificationService>();
        services.AddTransient<IEmailService, SendinBlueEmailService>();
        services.AddTransient<IMonitoringService, MonitoringService>();

        return services;
    }
}
