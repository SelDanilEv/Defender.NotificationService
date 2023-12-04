using Defender.Common.Exstension;
using Defender.NotificationService.Application.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.NotificationService.Application.Configuration.Exstension;

public static class ServiceOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SendinBlueOptions>(configuration.GetSection(nameof(SendinBlueOptions)));

        services.Configure<SenderInfoOptions>(configuration.GetSection(nameof(SenderInfoOptions)));

        services.Configure<NotificationSettingOptions>(configuration.GetSection(nameof(NotificationSettingOptions)));

        return services;
    }
}