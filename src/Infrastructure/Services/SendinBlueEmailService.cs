using AutoMapper;
using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.Common.Models;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Common.Interfaces.Repositories;
using Defender.NotificationService.Application.Common.Interfaces.Wrapper;
using Defender.NotificationService.Application.Configuration.Options;
using Defender.NotificationService.Application.Models;
using Defender.NotificationService.Domain.Entities;
using Defender.NotificationService.Domain.Enum;
using Microsoft.Extensions.Options;

namespace Defender.NotificationService.Infrastructure.Services;

public class SendinBlueEmailService : IEmailService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IEmailServiceWrapper _emailServiceWrapper;
    private readonly IMapper _mapper;
    private readonly NotificationSettingOptions _settings;

    public SendinBlueEmailService(
        IOptions<NotificationSettingOptions> settings,
        IEmailServiceWrapper emailServiceWrapper,
        INotificationRepository notificationRepository,
        IMapper mapper)
    {
        _settings = settings.Value;
        _emailServiceWrapper = emailServiceWrapper;
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<NotificationResponse> SendEmailAsync(string email, string subject, string body)
    {
        var notification = Notification.InitEmailNotificaton();
        notification.FillNotificatonData(email, subject, body);

        await _notificationRepository.CreateNotificationAsync(notification);

        var updateRequest = UpdateModelRequest<Notification>
            .Init(notification);

        try
        {

            var externalNotificationId = String.Empty;

            if (_settings.SendFakeEmail)
            {
                externalNotificationId = await _emailServiceWrapper.SendPlaintextEmailAsync(email, subject, body);
            }

            updateRequest
                .UpdateField(x => x.ExternalNotificationId, externalNotificationId)
                .UpdateField(x => x.Status, NotificationStatus.Sent);

            await _notificationRepository.UpdateNotificationAsync(notification.Id, updateRequest);
        }
        catch (Exception ex)
        {
            updateRequest.UpdateField(x => x.Status, NotificationStatus.Failed);
            await _notificationRepository.UpdateNotificationAsync(notification.Id, updateRequest);

            throw new ServiceException(ErrorCode.ES_SendinBlueIssue, ex);
        }

        return _mapper.Map<NotificationResponse>(notification);
    }
}
