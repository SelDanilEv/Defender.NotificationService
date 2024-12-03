using Defender.NotificationService.Application.Common.Interfaces.Services;
using Defender.NotificationService.Application.Common.Interfaces.Wrapper;
using Defender.NotificationService.Application.Configuration.Options;
using Defender.NotificationService.Application.Models;
using Microsoft.Extensions.Options;

namespace Defender.NotificationService.Application.Services;

public class SendinBlueEmailService(
        IOptions<NotificationSettingOptions> settings,
        IEmailServiceWrapper emailServiceWrapper)
    : IEmailService
{
    private readonly NotificationSettingOptions _settings = settings.Value;

    public async Task<string> SendEmailAsync(NotificationRequest request)
    {
        var externalNotificationId = String.Empty;

        if (!_settings.SendFakeEmail)
        {
            externalNotificationId = await emailServiceWrapper.SendPlaintextEmailAsync(
                request.Recipient,
                request.Subject,
                request.Body);
        }

        return externalNotificationId;
    }
}
