using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class ConferenceXattendee
{
    public int Id { get; set; }

    public string AttendeeEmail { get; set; } = null!;

    public int ConferenceId { get; set; }

    public int StatusId { get; set; }

    public virtual Conference Conference { get; set; } = null!;

    public virtual DictionaryStatus Status { get; set; } = null!;
}
