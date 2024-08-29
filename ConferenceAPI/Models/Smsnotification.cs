using System;

namespace ConferenceAPI.Models
{
    public partial class Smsnotification : Notification
    {
        public string PhoneNumber { get; set; } = null!;

        public Smsnotification() : base(null, string.Empty, string.Empty) { }

        public Smsnotification(string phoneNumber, string? message, string participantTemplate, string speakerTemplate)
            : base(message, participantTemplate, speakerTemplate)
        {
            PhoneNumber = phoneNumber;
        }
        public Smsnotification(string phoneNumber, string message)
            : base(message, "ParticipantTemplate", string.Empty)
        {
            PhoneNumber = phoneNumber;
        }

        public Smsnotification(string phoneNumber, string message, bool isSpeaker)
            : base(message, string.Empty, isSpeaker ? "SpeakerTemplate" : "ParticipantTemplate")
        {
            PhoneNumber = phoneNumber;
        }
    }
}
