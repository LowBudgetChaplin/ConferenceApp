using ConferenceAPI.Models;

namespace ConferenceAPI.Services
{
    public class SmsService : INotificationService
    {
        public void Send(Notification notification)
        {
            //cast notification as SmsNotification
            if (notification is Smsnotification smsNotification)
            {
                //
                Console.WriteLine($"Sending SMS: {smsNotification.Message}");
            }
            else
            {
                throw new InvalidCastException("Notification is not of type SmsNotification.");
            }
        }
    }
}
