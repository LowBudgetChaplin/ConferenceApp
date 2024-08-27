using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Smsnotification : Notification
{
    public int Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public Smsnotification(string phoneNumber, string? message, DateTime? sentDate, string participantTemplate, string speakerTemplate)
            : base(message, sentDate, participantTemplate, speakerTemplate)
    {
        PhoneNumber = phoneNumber;
    }
}
