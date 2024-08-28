using ConferenceAPI.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace ConferenceAPI.Services
{
    public class EmailService : INotificationService
    {
        public void Send(Notification notification)
        {
            // Cast notification to EmailNotification
            var emailNotification = notification as EmailNotification;

            if (emailNotification != null)
            {
                using (var client = new SmtpClient("localhost"))
                {
                    client.Port = 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("", "");
                    client.EnableSsl = false;


                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("luca.cordus@totalsoft.ro"),
                        Subject = emailNotification.Subject,
                        Body = emailNotification.Message,
                        IsBodyHtml = false
                    };

                    mailMessage.To.Add("serban.corodescu@totalsoft.ro");

                    try
                    {
                        client.Send(mailMessage);
                        emailNotification.SentDate = DateTime.Now;
                        Console.WriteLine("Email sent ");
                    }

                    catch (Exception ex)
                    {
                        throw new Exception("Email not sent", ex);
                    }
                }
            }
            else
            {
                throw new InvalidCastException("Notification is not the correct data type");
            }
        }
    }
}
