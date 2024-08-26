using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class FeedbackDeleted
{
    public int? Id { get; set; }

    public int? ConferenceId { get; set; }

    public string? AttendeeEmail { get; set; }

    public int? Rating { get; set; }

    public string? Message { get; set; }
}
