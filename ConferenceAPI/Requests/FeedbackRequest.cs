using ConferenceAPI.Models;
using System.Text.Json.Serialization;

namespace ConferenceAPI.Requests
{
    public class FeedbackRequest
    {
        public string? AttendeeEmail { get; set; }

        public int ConferenceId { get; set; }

        public int SpeakerId { get; set; }

        public decimal? Rating { get; set; }

        public string? Message { get; set; }

        public FeedbackRequest(string AttendeeEmail, int ConferenceId, int SpeakerId, decimal? Rating, string Message)
        {
            this.AttendeeEmail = AttendeeEmail;
            this.ConferenceId = ConferenceId;
            this.SpeakerId = SpeakerId;
            this.Rating = Rating;
            this.Message = Message;
        }
    }
}
