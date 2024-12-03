using AutoMapper;
using Defender.Common.Wrapper;
using Defender.NotificationService.Application.Common.Interfaces.Wrapper;
using Defender.NotificationService.Application.Configuration.Options;
using Defender.NotificationService.Infrastructure.Clients.SendinBlueClient.Generated;
using Microsoft.Extensions.Options;

namespace Defender.NotificationService.Infrastructure.Clients.SendinBlueClient;

public class SendinBlueServiceWrapper : BaseSwaggerWrapper, IEmailServiceWrapper
{
    private readonly IMapper _mapper;
    private readonly ISendinBlueClient _sendinBlueClient;
    private readonly SenderInfoOptions _senderInfoOptions;

    public SendinBlueServiceWrapper(
        IOptions<SenderInfoOptions> options,
        ISendinBlueClient sendinBlueClient,
        IMapper mapper)
    {
        _sendinBlueClient = sendinBlueClient;
        _mapper = mapper;
        _senderInfoOptions = options.Value;
    }

    public async Task<string> SendPlaintextEmailAsync(string recipient, string subject, string messageBody)
    {
        var message = new SendSmtpEmail()
        {
            Sender = new Sender7()
            {
                Name = _senderInfoOptions.Name,
                Email = _senderInfoOptions.Email
            },
            To = new List<To>() { new To() { Email = recipient } },
            Subject = subject,
            TextContent = messageBody
        };

        var result = await _sendinBlueClient.SendTransacEmailAsync(message);

        return result.MessageId;
    }
}
