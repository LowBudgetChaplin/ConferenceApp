using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Speaker
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Nationality { get; set; }

    public decimal? Rating { get; set; }

    public byte[]? Image { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<ConferenceXspeaker> ConferenceXspeakers { get; set; } = new List<ConferenceXspeaker>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
