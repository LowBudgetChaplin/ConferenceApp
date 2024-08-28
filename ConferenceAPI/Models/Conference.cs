using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Conference
{
    public int Id { get; set; }

    public int ConferenceTypeId { get; set; }

    public int LocationId { get; set; }

    public string OrganizerEmail { get; set; } = null!;

    public int CategoryId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Name { get; set; } = null!;

    public virtual DictionaryCategory Category { get; set; } = null!;

    public virtual DictionaryConferenceType ConferenceType { get; set; } = null!;

    public virtual ICollection<ConferenceXattendee> ConferenceXattendees { get; set; } = new List<ConferenceXattendee>();

    public virtual ICollection<ConferenceXspeaker> ConferenceXspeakers { get; set; } = new List<ConferenceXspeaker>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Location Location { get; set; } = null!;
}
