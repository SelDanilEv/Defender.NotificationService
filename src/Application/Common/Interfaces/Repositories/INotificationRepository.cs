using Defender.Common.Models;
using Defender.Common.Pagination;
using Defender.NotificationService.Domain.Entities;

namespace Defender.NotificationService.Application.Common.Interfaces.Repositories;

public interface INotificationRepository
{
    Task<Notification> GetNotificationsByIdAsync(Guid notificationId);
    Task<PagedResult<Notification>> GetNotificationsByRecipientAsync(PaginationSettings<Notification> settings);
    Task<Notification> CreateNotificationAsync(Notification notification);
    Task UpdateNotificationAsync(Guid id, UpdateModelRequest<Notification> updateModelRequest);
}
