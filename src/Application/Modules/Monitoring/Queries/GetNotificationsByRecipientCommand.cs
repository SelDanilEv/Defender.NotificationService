using AutoMapper;
using Defender.Common.DB.Pagination;
using Defender.Common.Errors;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Domain.Entities;
using FluentValidation;
using Defender.Common.Extension;
using MediatR;

namespace Defender.NotificationService.Application.Modules.Monitoring.Queries;

public record GetNotificationsByRecipientQuery : PaginationRequest, IRequest<PagedResult<Notification>>
{
    public string Recipient { get; set; }
};

public sealed class GetNotificationsByRecipientQueryValidator : AbstractValidator<GetNotificationsByRecipientQuery>
{
    public GetNotificationsByRecipientQueryValidator()
    {
        RuleFor(s => s.Recipient)
            .NotEmpty()
            .WithMessage(ErrorCode.VL_NTF_EmptyRecipient);
    }
}

public sealed class GetNotificationsByRecipientQueryHandler : IRequestHandler<GetNotificationsByRecipientQuery, PagedResult<Notification>>
{
    private readonly IMonitoringService _monitoringService;
    private readonly IMapper _mapper;

    public GetNotificationsByRecipientQueryHandler(
        IMonitoringService monitoringService,
        IMapper mapper
        )
    {
        _monitoringService = monitoringService;
        _mapper = mapper;
    }

    public async Task<PagedResult<Notification>> Handle(GetNotificationsByRecipientQuery request, CancellationToken cancellationToken)
    {
        var notifications = await _monitoringService.GetNotificationsByRecipientAsync(request, request.Recipient);

        return notifications;
    }
}
