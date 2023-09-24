namespace Defender.NotificationService.Application.Configuration.Options;

public record SenderInfoOptions
{
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
}
