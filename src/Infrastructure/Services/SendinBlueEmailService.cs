using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Common.Interfaces.Wrapper;
using Defender.NotificationService.Application.Configuration.Options;
using Defender.NotificationService.Application.Models;
using Microsoft.Extensions.Options;

namespace Defender.NotificationService.Infrastructure.Services;

public class SendinBlueEmailService : IEmailService
{
    private readonly IEmailServiceWrapper _emailServiceWrapper;
    private readonly NotificationSettingOptions _settings;

    public SendinBlueEmailService(
        IOptions<NotificationSettingOptions> settings,
        IEmailServiceWrapper emailServiceWrapper)
    {
        _settings = settings.Value;
        _emailServiceWrapper = emailServiceWrapper;
    }

    public async Task<string> SendEmailAsync(NotificationRequest request)
    {
        var externalNotificationId = String.Empty;

        if (!_settings.SendFakeEmail)
        {
            externalNotificationId = await _emailServiceWrapper.SendPlaintextEmailAsync(
                request.Recipient,
                request.Subject,
                request.Body);
        }

        return externalNotificationId;
    }
}
