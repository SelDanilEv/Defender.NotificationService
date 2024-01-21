using AutoMapper;
using Defender.Common.DB.Model;
using Defender.Common.DB.Pagination;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Common.Interfaces.Repositories;
using Defender.NotificationService.Domain.Entities;

namespace Defender.NotificationService.Infrastructure.Services;

public class MonitoringService : IMonitoringService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public MonitoringService(
        INotificationRepository notificationRepository,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<Notification> GetNotificationsByIdAsync(Guid notificationId)
    {
        return await _notificationRepository.GetNotificationsByIdAsync(notificationId);
    }

    public async Task<PagedResult<Notification>> GetNotificationsByRecipientAsync(PaginationRequest paginationRequest, string recipient)
    {
        var settings = PaginationSettings<Notification>.FromPaginationRequest(paginationRequest);

        var filterRequest = FindModelRequest<Notification>
            .Init(x => x.Recipient, recipient)
            .Sort(x=> x.CreatedDate, SortType.Desc);
        
        settings.SetupFindOptions(filterRequest);

        return await _notificationRepository.GetNotificationsByRecipientAsync(settings);
    }
}
