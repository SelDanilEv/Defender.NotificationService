using AutoMapper;
using Defender.Common.DB.Model;
using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.NotificationService.Application.Common.Interfaces;
using Defender.NotificationService.Application.Common.Interfaces.Repositories;
using Defender.NotificationService.Application.Models;
using Defender.NotificationService.Domain.Entities;
using Defender.NotificationService.Domain.Enum;

namespace Defender.NotificationService.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public NotificationService(
        INotificationRepository notificationRepository,
        IEmailService emailService,
        IMapper mapper)
    {
        _emailService = emailService;
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<NotificationResponse> SendNotificationAsync(NotificationRequest request)
    {
        var response = new NotificationResponse();

        var notification = Notification.InitEmailNotificaton();
        notification.FillNotificatonData(request.Recipient, request.Subject, request.Body);

        await _notificationRepository.CreateNotificationAsync(notification);

        var updateRequest = UpdateModelRequest<Notification>
            .Init(notification);

        try
        {
            var externalNotificationId = String.Empty;

            switch (request.Type)
            {
                case NotificationType.Email:
                    externalNotificationId = await _emailService.SendEmailAsync(request);
                    break;
                case NotificationType.SMS:
                    break;
            }

            updateRequest
                .UpdateField(x => x.ExternalNotificationId, externalNotificationId)
                .UpdateField(x => x.Status, NotificationStatus.Sent);
        }
        catch (Exception ex)
        {
            updateRequest.UpdateField(x => x.Status, NotificationStatus.Failed);

            throw new ServiceException(ErrorCode.ES_SendinBlueIssue, ex);
        }
        finally
        {
            await _notificationRepository.UpdateNotificationAsync(notification.Id, updateRequest);
        }


        return _mapper.Map<NotificationResponse>(notification);
    }
}
