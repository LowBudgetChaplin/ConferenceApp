using System;

namespace ConferenceAPI.Models
{
    //constructor fara parametri pt entity framework
    //constructor with params to create participant email notification - use template for message
    //constructor with params to create speaker email notification - use template for message
    public partial class EmailNotification : Notification
    {
        public string To { get; set; } = null!;
        public string Cc { get; set; } = null!;
        public string Subject { get; set; } = null!;

        public EmailNotification() { }

        public EmailNotification(string to, string cc, string message, string subject, string participantTemplate, string speakerTemplate)
        {
            To = to;
            Cc = cc;
            Subject = subject;
        }

        public EmailNotification(string attendeeName, string conferenceName, string speakerNames, string location, string to, string cc, string subject)
        {
            To = to;
            Cc = cc;
            Subject = subject;
            Message = FormatParticipantMessage(attendeeName, conferenceName, speakerNames, location);
        }

        public EmailNotification(string speakerName, string conferenceName, DateTime startDate, string location, string to, string cc, string subject)
        {
            To = to;
            Cc = cc;
            Subject = subject;
            Message = FormatSpeakerMessage(speakerName, conferenceName, startDate, location);
        }

        private string FormatParticipantMessage(string participantName, string conferenceName, string speakerNames, string location, string to, string cc, string PartipateTemplate )
        {
            return string.Format("Hello, {0}! You have been enrolled in the course {1}. It will be led by {2}, starting on {3} at {4}, located at {5}.",
                participantName, conferenceName, speakerNames, DateTime.Now.ToString("f"), location);
        }

        private string FormatSpeakerMessage(string speakerName, string conferenceName, DateTime startDate, string location)
        {
            return string.Format("Hello, {0}! You have been assigned as a speaker for the course {1}, scheduled for {2} at {3}, at {4}.",
                speakerName, conferenceName, startDate.ToString("f"), location);
        }
    }
}
