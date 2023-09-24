using Defender.NotificationService.Domain.Enum;

namespace Defender.NotificationService.Application.Models;

public record NotificationResponse
{
    public Guid Id { get; set; }
    public NotificationType Type { get; set; }
    public NotificationStatus Status { get; set; }
    public string? Recipient { get; set; }
    public string? ExternalNotificationId { get; set; }
}
