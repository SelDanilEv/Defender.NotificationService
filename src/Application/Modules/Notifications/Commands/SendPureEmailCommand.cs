using Defender.Common.Errors;
using Defender.NotificationService.Application.Common.Interfaces;
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
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_EmptyRecipient))
                  .EmailAddress().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_InvalidEmail));

        RuleFor(s => s.Subject)
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_EmptySubject))
                  .MinimumLength(ValidationConstants.MinSubjectLength).WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_MinSubjectLength))
                  .MaximumLength(ValidationConstants.MaxSubjectLength).WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_MaxSubjectLength));

        RuleFor(s => s.Body)
                  .NotEmpty().WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_EmptyBody))
                  .MinimumLength(ValidationConstants.MaxBodyLength).WithMessage(ErrorCodeHelper.GetErrorCode(ErrorCode.VL_NTF_MaxBodyLength));
    }
}

public sealed class SendPureEmailCommandHandler : IRequestHandler<SendPureEmailCommand, NotificationResponse>
{
    private readonly IEmailService _emailService;

    public SendPureEmailCommandHandler(
        IEmailService emailService
        )
    {
        _emailService = emailService;
    }

    public async Task<NotificationResponse> Handle(SendPureEmailCommand request, CancellationToken cancellationToken)
    {
        return await _emailService.SendEmailAsync(request.RecipientEmail, request.Subject, request.Body);
    }
}
