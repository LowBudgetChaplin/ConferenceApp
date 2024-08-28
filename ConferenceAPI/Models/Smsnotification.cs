using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Smsnotification : Notification
{
    public string PhoneNumber { get; set; } = null!;

    public Smsnotification() { }

    public Smsnotification(string phoneNumber, string? message, DateTime? sentDate, string participantTemplate, string speakerTemplate)
    {
        PhoneNumber = phoneNumber;
    }



    //constructor fara parametri pt entity framework
    //constructor with params to create participant sms notification - use template for message  
    //constructor with params to create speaker sms notification - use template for message  
}
