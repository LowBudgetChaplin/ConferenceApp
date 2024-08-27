using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class EmailNotification : Notification
{

    public int Id { get; set; }

    public string To { get; set; } = null!;

    public string Cc { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public EmailNotification(string to, string cc, string? message, string subject, DateTime? sentDate, string participantTemplate, string speakerTemplate) 
        : base(message, sentDate, participantTemplate, speakerTemplate)
    {
        To = to;
        Cc = cc;
        Subject = subject;
    }
}