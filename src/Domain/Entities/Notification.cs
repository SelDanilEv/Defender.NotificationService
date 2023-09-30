using Defender.Common.Entities;
using Defender.NotificationService.Domain.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Defender.NotificationService.Domain.Entities;

public class Notification : IBaseModel
{

    public Guid Id { get; set; }

    [BsonRepresentation(BsonType.String)]
    public NotificationType Type { get; set; }
    [BsonRepresentation(BsonType.String)]
    public NotificationStatus Status { get; set; }

    public string Recipient { get; set; }
    public string Header { get; set; }
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }

    public string ExternalNotificationId { get; set; }


    public Notification()
    {
        CreatedDate = DateTime.UtcNow;
    }

    public static Notification InitEmailNotificaton()
    {
        return new Notification()
        {
            Type = NotificationType.Email,
            Status = NotificationStatus.PreparingToSend,
        };
    }

    public static Notification InitSMSNotificaton()
    {
        return new Notification()
        {
            Type = NotificationType.SMS,
            Status = NotificationStatus.PreparingToSend,
        };
    }

    public Notification FillNotificatonData(string recipient, string subject, string body)
    {
        this.Recipient = recipient;
        this.Header = subject;
        this.Message = body;

        return this;
    }

}
