using Defender.NotificationService.Application.Models;

namespace Defender.NotificationService.Application.Common.Interfaces;

public interface IEmailService
{
    Task<NotificationResponse> SendEmailAsync(string email, string subject, string message);
}
