using Defender.Common.DB.Pagination;
using Defender.Common.Mapping;
using Defender.NotificationService.Application.Models;
using Defender.NotificationService.Domain.Entities;

namespace Defender.NotificationService.Application.Common.Mappings;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {
        CreateMap<Notification, NotificationResponse>();
        CreateMap<PagedResult<Notification>, PagedResult<NotificationResponse>>();
    }
}
