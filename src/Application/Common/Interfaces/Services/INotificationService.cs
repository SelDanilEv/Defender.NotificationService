using Defender.NotificationService.Application.Models;

namespace Defender.NotificationService.Application.Common.Interfaces.Services;

public interface INotificationService
{
    Task<NotificationResponse> SendNotificationAsync(NotificationRequest request);
}
