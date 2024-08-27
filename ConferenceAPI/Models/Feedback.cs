using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConferenceAPI.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string? AttendeeEmail { get; set; }

    public int ConferenceId { get; set; }

    public int SpeakerId { get; set; }

    public decimal? Rating { get; set; }

    public string? Message { get; set; }

    public virtual Conference Conference { get; set; } = null!;

    [JsonIgnore]
    public virtual Speaker Speaker { get; set; } = null!;
    public Feedback(string AttendeeEmail, int ConferenceId, int SpeakerId, decimal? Rating, string Message)
    {
        this.AttendeeEmail = AttendeeEmail;
        this.ConferenceId = ConferenceId;
        this.SpeakerId = SpeakerId;
        this.Rating = Rating;
        this.Message = Message;
    }
}
