using Defender.Common.Errors;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Models;
using FluentValidation;
using MediatR;

namespace Defender.NotificationService.Application.Modules.Notifications.Commands;

public record SendVerificationEmailCommand : IRequest<NotificationResponse>
{
    public string RecipientEmail { get; set; }
    public string VerificationLink { get; set; }
};

public sealed class SendVerificationEmailCommandValidator : AbstractValidator<SendVerificationEmailCommand>
{
    public SendVerificationEmailCommandValidator()
    {
        RuleFor(s => s.RecipientEmail)
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_EmptyRecipient))
                  .EmailAddress().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_InvalidEmail));

        RuleFor(s => s.VerificationLink)
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_EmptyValidationLink));
    }
}

public sealed class SendVerificationEmailCommandHandler : IRequestHandler<SendVerificationEmailCommand, NotificationResponse>
{
    private readonly INotificationService _notificationService;

    public SendVerificationEmailCommandHandler(
        INotificationService notificationService
        )
    {
        _notificationService = notificationService;
    }

    public async Task<NotificationResponse> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
    {
        var notificationRequest = NotificationRequest.VerificationEmail(request.RecipientEmail, request.VerificationLink);

        return await _notificationService.SendNotificationAsync(notificationRequest);
    }
}
