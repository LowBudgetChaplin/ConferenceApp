using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Smsnotification
{
    public int Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime SentDate { get; set; }
}
