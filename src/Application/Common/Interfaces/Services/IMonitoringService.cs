using Defender.Common.Pagination;
using Defender.NotificationService.Domain.Entities;

namespace Defender.NotificationService.Application.Common.Interfaces;

public interface IMonitoringService
{
    Task<Notification> GetNotificationsByIdAsync(Guid notificationId);
    Task<PagedResult<Notification>> GetNotificationsByRecipientAsync(string recipient, PaginationSettings<Notification> settings);
}
