namespace ConferenceAPI.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime SentDate { get; set; }
        public string ParticipantTemplate { get; set; } = "Hello, {0}! You have been enrolled in the course {1}. It will be led by {2}, starting on {3} at {4}, located at {5}";
        public string SpeakerTemplate { get; set; } = "Hello, {0}! You have been assigned as a speaker for the course {1}, scheduled for {2} at {3}, at {4}";

        public Notification(string? message,  string participantTemplate, string speakerTemplate)
        {
            Message = message;
            ParticipantTemplate = participantTemplate;
            SpeakerTemplate = speakerTemplate;
        }

        public string FormatParticipantMessage(string participantName, string conferenceName, string speakerNames, DateTime date, string location)
        {
            return string.Format(ParticipantTemplate,participantName, conferenceName, speakerNames, date.ToString("MM/dd/yyyy"), date.ToString("HH:mm"), location);
        }

        public string FormatSpeakerMessage(string speakerName, string conferenceName, string location)
        {
            return string.Format(SpeakerTemplate, speakerName, conferenceName, location);
        }
    }
}