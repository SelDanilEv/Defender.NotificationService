namespace Defender.NotificationService.Application.Configuration.Options;

public record NotificationSettingOptions
{
    public bool SendFakeEmail { get; set; } = true;
}
