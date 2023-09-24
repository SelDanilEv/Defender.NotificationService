using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.NotificationService.Application.Models;
using Defender.Common.Attributes;
using Defender.Common.Models;
using Defender.NotificationService.Application.Modules.Notifications.Commands;

namespace Defender.NotificationService.WebUI.Controllers.V1;

public class NotificationController : BaseApiController
{
    public NotificationController(IMediator mediator, IMapper mapper) 
        : base(mediator, mapper)
    {
    }

    [HttpPost("send/email")]
    //[Auth(Roles.Admin)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<NotificationResponse> SendPureMessageAsync([FromBody] SendPureEmailCommand command)
    {
        return await ProcessApiCallAsync<SendPureEmailCommand, NotificationResponse>(command);
    }
}
