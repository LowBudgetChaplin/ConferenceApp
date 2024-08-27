using ConferenceAPI.Models;

namespace ConferenceAPI.Services
{
    public class NotificationManager
    {
        readonly Dictionary<Type, INotificationService> _services;

        public NotificationManager()
        {
            _services = new Dictionary<Type, INotificationService>
    {
        { typeof(EmailNotification), new EmailService() },
        { typeof(Smsnotification), new SmsService() }
    };
        }

        public void SendNotification(Notification notification)
        {
            var type = notification.GetType();
            if (_services.ContainsKey(type))
            {
                _services[type].Send(notification);
            }
            else
            {
                throw new NotSupportedException("Notification type not supported.");
            }
        }
    }
}
