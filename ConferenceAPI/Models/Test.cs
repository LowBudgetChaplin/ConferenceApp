using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Test
{
    public int Id { get; set; }

    public string Info { get; set; } = null!;

    public DateTime? RecordStamp { get; set; }
}
