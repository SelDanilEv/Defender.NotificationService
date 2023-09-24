using AutoMapper;
using Defender.Common.Pagination;
using Defender.NotificationService.Application.Models;
using Defender.NotificationService.Domain.Entities;

namespace Defender.NotificationService.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Notification, NotificationResponse>();
        CreateMap<PagedResult<Notification>, PagedResult<NotificationResponse>>();
    }
}