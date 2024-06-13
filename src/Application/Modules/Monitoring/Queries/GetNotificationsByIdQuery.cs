using AutoMapper;
using Defender.Common.Errors;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Domain.Entities;
using FluentValidation;
using Defender.Common.Extension;
using MediatR;

namespace Defender.NotificationService.Application.Modules.Monitoring.Queries;

public record GetNotificationsByIdQuery : IRequest<Notification>
{
    public Guid NotificationId { get; set; }
};

public sealed class GetNotificationsByIdQueryValidator : AbstractValidator<GetNotificationsByIdQuery>
{
    public GetNotificationsByIdQueryValidator()
    {
        RuleFor(s => s.NotificationId)
            .NotEmpty()
            .WithMessage(ErrorCode.VL_NTF_EmptyNotificationId);
    }
}

public sealed class GetNotificationsByIdQueryHandler : IRequestHandler<GetNotificationsByIdQuery, Notification>
{
    private readonly IMonitoringService _monitoringService;
    private readonly IMapper _mapper;

    public GetNotificationsByIdQueryHandler(
        IMonitoringService monitoringService,
        IMapper mapper
        )
    {
        _monitoringService = monitoringService;
        _mapper = mapper;
    }

    public async Task<Notification> Handle(GetNotificationsByIdQuery request, CancellationToken cancellationToken)
    {
        var notification = await _monitoringService.GetNotificationsByIdAsync(request.NotificationId);

        return notification;
    }
}
