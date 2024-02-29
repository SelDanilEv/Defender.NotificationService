using Defender.Common.DB.Model;
using Defender.Common.DB.Pagination;
using Defender.NotificationService.Domain.Entities;

namespace Defender.NotificationService.Application.Common.Interfaces.Repositories;

public interface INotificationRepository
{
    Task<Notification> GetNotificationsByIdAsync(Guid notificationId);
    Task<PagedResult<Notification>> GetNotificationsAsync(PaginationSettings<Notification> settings);
    Task<Notification> CreateNotificationAsync(Notification notification);
    Task UpdateNotificationAsync(Guid id, UpdateModelRequest<Notification> updateModelRequest);
}
