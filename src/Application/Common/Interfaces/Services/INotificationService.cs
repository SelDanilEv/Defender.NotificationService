using Defender.NotificationService.Application.Models;

namespace Defender.NotificationService.Application.Common.Interfaces;

public interface INotificationService
{
    Task<NotificationResponse> SendNotificationAsync(NotificationRequest request);
}
