using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Smsnotification : Notification
{
    public string PhoneNumber { get; set; } = null!;

    public Smsnotification() : base(null, string.Empty, string.Empty) { }

    public Smsnotification(string phoneNumber, string? message, string participantTemplate, string speakerTemplate)
        : base(message, participantTemplate, speakerTemplate)
    {
        PhoneNumber = phoneNumber;
    }

    //constructor fara parametri pt entity framework
    //constructor with params to create participant sms notification - use template for message  
    //constructor with params to create speaker sms notification - use template for message  
}
