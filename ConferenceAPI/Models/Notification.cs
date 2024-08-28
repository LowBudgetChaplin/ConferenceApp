namespace ConferenceAPI.Models
{
    public class Notification
    {
        private DateTime? sentDate;
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime SentDate { get; set; }
        public string ParticipantTemplate { get; set; } = "Hello, {0}! You have been enrolled in the course {1}. It will be led by {2}, starting on {3} at {4}, located at {5}";
        public string SpeakerTemplate { get; set; } = "Hello, {0}! You have been assigned as a speaker for the course {1}, scheduled for {2} at {3}, at {4}";

        public Notification() { }

        public string FormatParticipantMessage(string participantName, string conferenceName, string speakerNames, string location)
        {
            return string.Format(ParticipantTemplate, participantName, conferenceName, speakerNames, location);
        }

        public string FormatSpeakerMessage(string speakerName, string conferenceName, string location)
        {
            return string.Format(SpeakerTemplate, speakerName, conferenceName, location);
        }

        //template mesaj participant sa includa numele particpantului, denumirea conferintei, nume sustinatorului/lor(speaker), data de inceput, ora, locatia
        //template mesaj speaker sa includa nume speaker, denumire conferinta, data, ora locatia
    }
}