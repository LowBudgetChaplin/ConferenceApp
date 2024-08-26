using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class DictionaryStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<ConferenceXattendee> ConferenceXattendees { get; set; } = new List<ConferenceXattendee>();
}
