﻿using System.Net.Http.Headers;
using System.Reflection;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Common.Interfaces.Repositories;
using Defender.NotificationService.Application.Common.Interfaces.Wrapper;
using Defender.NotificationService.Application.Configuration.Options;
using Defender.NotificationService.Infrastructure.Clients.SendinBlueClient;
using Defender.NotificationService.Infrastructure.Clients.ServiceClient.Generated;
using Defender.NotificationService.Infrastructure.Helpers.LocalSecretHelper;
using Defender.NotificationService.Infrastructure.Repositories.Notifications;
using Defender.NotificationService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.NotificationService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        RegisterServices(services);

        RegisterRepositories(services);

        RegisterApiClients(services, configuration);

        RegisterClientWrappers(services);

        return services;
    }

    private static void RegisterClientWrappers(IServiceCollection services)
    {
        services.AddTransient<IEmailServiceWrapper, SendinBlueServiceWrapper>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<INotificationService, Services.NotificationService>();
        services.AddTransient<IEmailService, SendinBlueEmailService>();
        services.AddTransient<IMonitoringService, MonitoringService>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddSingleton<INotificationRepository, NotificationRepository>();
    }

    private static void RegisterApiClients(IServiceCollection services, IConfiguration configuration)
    {
        services.PostConfigure<SendinBlueOptions>(option =>
        {
            option.ApiKey = LocalSecretsHelper.GetSecret(LocalSecret.EmailApiKey);
        });

        services.AddHttpClient<ISendinBlueClient, SendinBlueClient>(nameof(SendinBlueClient), (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SendinBlueOptions>>().Value;

            client.BaseAddress = new Uri(options.Url);

            client.DefaultRequestHeaders.Add("api-key", options.ApiKey);
            client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
    }

}
