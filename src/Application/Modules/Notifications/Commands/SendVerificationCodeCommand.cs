using Defender.Common.Errors;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Models;
using FluentValidation;
using Defender.Common.Extension;
using MediatR;

namespace Defender.NotificationService.Application.Modules.Notifications.Commands;

public record SendVerificationCodeCommand : IRequest<NotificationResponse>
{
    public string RecipientEmail { get; set; }
    public int Code { get; set; }
};

public sealed class SendVerificationCodeCommandValidator : AbstractValidator<SendVerificationCodeCommand>
{
    public SendVerificationCodeCommandValidator()
    {
        RuleFor(s => s.RecipientEmail)
            .NotEmpty().WithMessage(ErrorCode.VL_NTF_EmptyRecipient)
            .EmailAddress().WithMessage(ErrorCode.VL_NTF_InvalidEmail);
    }
}

public sealed class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, NotificationResponse>
{
    private readonly INotificationService _notificationService;

    public SendVerificationCodeCommandHandler(
        INotificationService notificationService
        )
    {
        _notificationService = notificationService;
    }

    public async Task<NotificationResponse> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var notificationRequest = NotificationRequest.VerificationCode(request.RecipientEmail, request.Code);

        return await _notificationService.SendNotificationAsync(notificationRequest);
    }
}
