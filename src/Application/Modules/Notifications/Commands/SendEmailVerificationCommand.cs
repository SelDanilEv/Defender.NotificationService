using Defender.Common.Errors;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Defender.NotificationService.Application.Modules.Notifications.Commands;

public record SendEmailVerificationCommand : IRequest<NotificationResponse>
{
    public string? RecipientEmail { get; set; }
    public int Hash { get; set; }
    public int Code { get; set; }
};

public sealed class SendVerificationEmailCommandValidator : AbstractValidator<SendEmailVerificationCommand>
{
    public SendVerificationEmailCommandValidator()
    {
        RuleFor(s => s.RecipientEmail)
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_EmptyRecipient))
                  .EmailAddress().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_InvalidEmail));
    }
}

public sealed class SendVerificationEmailCommandHandler : IRequestHandler<SendEmailVerificationCommand, NotificationResponse>
{
    private readonly INotificationService _notificationService;
    private readonly IConfiguration _configuration;

    public SendVerificationEmailCommandHandler(
        INotificationService notificationService,
        IConfiguration configuration
        )
    {
        _notificationService = notificationService;
        _configuration = configuration;
    }

    public async Task<NotificationResponse> Handle(SendEmailVerificationCommand request, CancellationToken cancellationToken)
    {
        var verificationLink = CreateVerificationLink(request.Hash, request.Code);

        var notificationRequest = NotificationRequest.EmailVerification(
            request.RecipientEmail,
            verificationLink);

        return await _notificationService.SendNotificationAsync(notificationRequest);
    }

    private string CreateVerificationLink(int hash, int code) =>
        String.Format(_configuration["VerificationEmailLinkTemplate"],
            hash,
            code);
}
