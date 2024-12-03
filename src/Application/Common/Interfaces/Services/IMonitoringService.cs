using Defender.Common.DB.Pagination;
using Defender.NotificationService.Domain.Entities;

namespace Defender.NotificationService.Application.Common.Interfaces.Services;

public interface IMonitoringService
{
    Task<Notification> GetNotificationsByIdAsync(Guid notificationId);
    Task<PagedResult<Notification>> GetNotificationsByRecipientAsync(PaginationRequest settings, string recipient);
}
