using ConferenceAPI.Models;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ConferenceAPI.Services
{
    public class SmsService : INotificationService
    {
        public void Send(Notification notification)
        {
            //cast notification as SmsNotification
            if (notification is Smsnotification smsNotification)
            {
                try
                {
                    var message = MessageResource.Create(
                        body: smsNotification.Message,
                        from: new PhoneNumber("+16504605145"),
                        to: smsNotification.PhoneNumber
                    );

                    Console.WriteLine($"Message SID: {message.Sid}");

                    smsNotification.SentDate = DateTime.Now;
                    Console.WriteLine("SMS sent successfully");
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to send SMS", ex);
                }
            }
        }
    }
}
