using Defender.Common.Configuration.Options;
using Defender.Common.DB.Model;
using Defender.Common.DB.Pagination;
using Defender.Common.DB.Repositories;
using Defender.NotificationService.Application.Common.Interfaces.Repositories;
using Defender.NotificationService.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Defender.NotificationService.Infrastructure.Repositories.Notifications;

public class NotificationRepository : BaseMongoRepository<Notification>, INotificationRepository
{
    public NotificationRepository(IOptions<MongoDbOptions> mongoOption) : base(mongoOption.Value)
    {
    }

    public async Task<Notification> GetNotificationsByIdAsync(Guid notificationId)
    {
        return await GetItemAsync(notificationId);
    }

    public async Task<PagedResult<Notification>> GetNotificationsByRecipientAsync(PaginationSettings<Notification> settings)
    {
        return await GetItemsAsync(settings);
    }

    public async Task<Notification> CreateNotificationAsync(Notification user)
    {
        return await AddItemAsync(user);
    }

    public async Task UpdateNotificationAsync(Guid id, UpdateModelRequest<Notification> updateModelRequest)
    {
        var filter = this.CreateIdFilter(id);

        var updateDefinition = updateModelRequest.BuildUpdateDefinition();

        await _mongoCollection.UpdateOneAsync(filter, updateDefinition);
    }
}
