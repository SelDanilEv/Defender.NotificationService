using System.Net.Http.Headers;
using System.Reflection;
using Defender.NotificationService.Application.Common.Interfaces.Repositories;
using Defender.NotificationService.Application.Common.Interfaces.Wrapper;
using Defender.NotificationService.Application.Configuration.Options;
using Defender.NotificationService.Application.Helpers.LocalSecretHelper;
using Defender.NotificationService.Infrastructure.Clients.SendinBlueClient;
using Defender.NotificationService.Infrastructure.Clients.SendinBlueClient.Generated;
using Defender.NotificationService.Infrastructure.Repositories.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.NotificationService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services
            .RegisterRepositories()
            .RegisterApiClients(configuration)
            .RegisterClientWrappers();

        return services;
    }

    private static IServiceCollection RegisterClientWrappers(this IServiceCollection services)
    {
        services.AddTransient<IEmailServiceWrapper, SendinBlueServiceWrapper>();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<INotificationRepository, NotificationRepository>();

        return services;
    }

    private static IServiceCollection RegisterApiClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.PostConfigure<SendinBlueOptions>(option =>
        {
            option.ApiKey = LocalSecretsHelper
            .GetSecretAsync(LocalSecret.EmailApiKey)
            .Result;
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

        return services;
    }

}
