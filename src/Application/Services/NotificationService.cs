using AutoMapper;
using Defender.Common.DB.Model;
using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.NotificationService.Application.Common.Interfaces.Repositories;
using Defender.NotificationService.Application.Common.Interfaces.Services;
using Defender.NotificationService.Application.Models;
using Defender.NotificationService.Domain.Entities;
using Defender.NotificationService.Domain.Enum;

namespace Defender.NotificationService.Application.Services;

public class NotificationService(
        INotificationRepository notificationRepository,
        IEmailService emailService,
        IMapper mapper)
    : INotificationService
{
    public async Task<NotificationResponse> SendNotificationAsync(NotificationRequest request)
    {
        var response = new NotificationResponse();

        var notification = Notification.InitEmailNotificaton();
        notification.FillNotificatonData(request.Recipient, request.Subject, request.Body);

        await notificationRepository.CreateNotificationAsync(notification);

        var updateRequest = UpdateModelRequest<Notification>
            .Init(notification);

        try
        {
            var externalNotificationId = String.Empty;

            switch (request.Type)
            {
                case NotificationType.Email:
                    externalNotificationId = await emailService.SendEmailAsync(request);
                    break;
                case NotificationType.SMS:
                    break;
            }

            updateRequest
                .Set(x => x.ExternalNotificationId, externalNotificationId)
                .Set(x => x.Status, NotificationStatus.Sent);
        }
        catch (Exception ex)
        {
            updateRequest.Set(x => x.Status, NotificationStatus.Failed);

            throw new ServiceException(ErrorCode.ES_SendinBlueIssue, ex);
        }
        finally
        {
            await notificationRepository.UpdateNotificationAsync(notification.Id, updateRequest);
        }


        return mapper.Map<NotificationResponse>(notification);
    }
}
