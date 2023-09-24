namespace Defender.NotificationService.Application.Configuration.Options;

public record SendinBlueOptions
{
    public string Url { get; set; } = String.Empty;
    public string ApiKey { get; set; }
}
