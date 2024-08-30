using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConferenceAPI.Models;

public partial class ConferenceXspeaker
{
    public int Id { get; set; }

    public int ConferenceId { get; set; }

    public int SpeakerId { get; set; }

    public bool? IsMainSpeaker { get; set; }

    public virtual Conference Conference { get; set; } = null!;

    [JsonIgnore]
    public virtual Speaker Speaker { get; set; } = null!;

    public ConferenceXspeaker() { }

    public ConferenceXspeaker(int id, int conferenceId, int speakerId, bool? isMainSpeaker)
    {
        Id = id;
        ConferenceId = conferenceId;
        SpeakerId = speakerId;
        IsMainSpeaker = isMainSpeaker;
    }

}