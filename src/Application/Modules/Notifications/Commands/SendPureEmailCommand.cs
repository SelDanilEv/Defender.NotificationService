using Defender.Common.Errors;
using Defender.Common.Extension;
using Defender.NotificationService.Application.Common.Interfaces.Services;
using Defender.NotificationService.Application.Models;
using FluentValidation;
using MediatR;

namespace Defender.NotificationService.Application.Modules.Notifications.Commands;

public record SendPureEmailCommand : IRequest<NotificationResponse>
{
    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
};

public sealed class SendPureEmailCommandValidator : AbstractValidator<SendPureEmailCommand>
{
    public SendPureEmailCommandValidator()
    {
        RuleFor(s => s.RecipientEmail)
            .NotEmpty().WithMessage(ErrorCode.VL_NTF_EmptyRecipient)
            .EmailAddress().WithMessage(ErrorCode.VL_NTF_InvalidEmail);

        RuleFor(s => s.Subject)
            .NotEmpty().WithMessage(ErrorCode.VL_NTF_EmptySubject)
            .MinimumLength(ValidationConstants.MinSubjectLength)
            .WithMessage(ErrorCode.VL_NTF_MinSubjectLength)
            .MaximumLength(ValidationConstants.MaxSubjectLength)
            .WithMessage(ErrorCode.VL_NTF_MaxSubjectLength);

        RuleFor(s => s.Body)
            .NotEmpty().WithMessage(ErrorCode.VL_NTF_EmptyBody)
            .MinimumLength(ValidationConstants.MaxBodyLength)
            .WithMessage(ErrorCode.VL_NTF_MaxBodyLength);
    }
}

public sealed class SendPureEmailCommandHandler : IRequestHandler<SendPureEmailCommand, NotificationResponse>
{
    private readonly INotificationService _notificationService;

    public SendPureEmailCommandHandler(
        INotificationService notificationService
        )
    {
        _notificationService = notificationService;
    }

    public async Task<NotificationResponse> Handle(SendPureEmailCommand request, CancellationToken cancellationToken)
    {
        var notificationRequest = NotificationRequest.Email(request.RecipientEmail, request.Subject, request.Body);

        return await _notificationService.SendNotificationAsync(notificationRequest);
    }
}
