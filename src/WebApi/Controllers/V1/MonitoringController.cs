using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.NotificationService.Application.Models;
using Defender.Common.Attributes;
using Defender.Common.Models;
using Defender.Common.Pagination;
using Defender.NotificationService.Application.Modules.Monitoring.Queries;
using Defender.NotificationService.Domain.Entities;

namespace Defender.NotificationService.WebUI.Controllers.V1;

public class MonitoringController : BaseApiController
{
    public MonitoringController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [HttpGet("by-id")]
    [Auth(Roles.Admin)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<Notification> GetNotificationsByRecipientAsync(GetNotificationsByIdQuery command)
    {
        return await ProcessApiCallAsync<GetNotificationsByIdQuery, Notification>(command);
    }

    [HttpGet("by-recipient")]
    [Auth(Roles.Admin)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<PagedResult<NotificationResponse>> GetNotificationsByRecipientAsync(GetNotificationsByRecipientQuery command)
    {
        return await ProcessApiCallAsync<GetNotificationsByRecipientQuery, PagedResult<NotificationResponse>>(command);
    }
}
