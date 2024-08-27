namespace ConferenceAPI.Models
{
    public class Notification
    {
        private DateTime? sentDate;

        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime SentDate { get; set; }

        public string ParticipantTemplate { get; set; } = "Hello, {0}! You have been enrolled to the course {1}….";
        public string SpeakerTemplate { get; set; } = null;

        public Notification(string? message, DateTime? sentDate, string participantTemplate, string speakerTemplate)
        {
            Message = message;
            sentDate = sentDate;
            ParticipantTemplate = participantTemplate;
            SpeakerTemplate = speakerTemplate;
        }
    }
}