using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class EmailNotification
{
    public int Id { get; set; }

    public string To { get; set; } = null!;

    public string Cc { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime SentDate { get; set; }
}
