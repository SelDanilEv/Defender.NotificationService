using Defender.Common.DTOs;

namespace Defender.NotificationService.Application.Common.Interfaces.Wrapper;
public interface IEmailServiceWrapper
{
    Task<string> SendPlaintextEmailAsync(string recipient, string subject, string messageBody);
}
