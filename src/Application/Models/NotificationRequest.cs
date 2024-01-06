using Defender.NotificationService.Application.Resources;
using Defender.NotificationService.Domain.Enum;

namespace Defender.NotificationService.Application.Models;

public record NotificationRequest
{
    public string? Recipient { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public NotificationType? Type { get; set; }

    public NotificationRequest(NotificationType? type, string? recipient, string? subject, string? body)
    {
        Recipient = recipient;
        Subject = subject;
        Body = body;
        Type = type;
    }

    public static NotificationRequest Email(string? recipient, string? subject, string? body)
    {
        return new NotificationRequest(
            NotificationType.Email,
            recipient,
            subject,
            body);
    }

    public static NotificationRequest EmailVerification(string? recipient, string? verificationUrl)
    {
        return Email(
                recipient,
                Notifications.EmailVerification_Subject,
                string.Format(Notifications.EmailVerification_Body, verificationUrl));
    }

    public static NotificationRequest VerificationCode(string? recipient, int verificationCode)
    {
        return Email(
                recipient,
                Notifications.VerificationCode_Subject,
                string.Format(Notifications.VerificationCode_Body, verificationCode));
    }

}
