using System.Threading.Tasks;
using AutoMapper;
using Defender.Common.Attributes;
using Defender.Common.Consts;
using Defender.NotificationService.Application.Models;
using Defender.NotificationService.Application.Modules.Notifications.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class NotificationController : BaseApiController
{
    public NotificationController(IMediator mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [HttpPost("send/email")]
    [Auth(Roles.Admin)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<NotificationResponse> SendPureMessageAsync([FromBody] SendPureEmailCommand command)
    {
        return await ProcessApiCallAsync<SendPureEmailCommand, NotificationResponse>(command);
    }

    [HttpPost("send/email-verification")]
    [Auth(Roles.Admin)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<NotificationResponse> SendVerificationMessageAsync([FromBody] SendEmailVerificationCommand command)
    {
        return await ProcessApiCallAsync<SendEmailVerificationCommand, NotificationResponse>(command);
    }

    [HttpPost("send/verification-code")]
    [Auth(Roles.Admin)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<NotificationResponse> SendVerificationCodeAsync([FromBody] SendVerificationCodeCommand command)
    {
        return await ProcessApiCallAsync<SendVerificationCodeCommand, NotificationResponse>(command);
    }
}
