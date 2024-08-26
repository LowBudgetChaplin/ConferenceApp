using System;
using System.Collections.Generic;

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

    public virtual Speaker Speaker { get; set; } = null!;
}
