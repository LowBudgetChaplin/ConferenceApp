using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class ConferenceXspeaker
{
    public int Id { get; set; }

    public int ConferenceId { get; set; }

    public int SpeakerId { get; set; }

    public bool? IsMainSpeaker { get; set; }

    public virtual Conference Conference { get; set; } = null!;

    public virtual Speaker Speaker { get; set; } = null!;
}
