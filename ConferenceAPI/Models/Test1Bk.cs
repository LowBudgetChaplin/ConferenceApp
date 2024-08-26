using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Test1Bk
{
    public int Id { get; set; }

    public string Info { get; set; } = null!;

    public DateTime? RecordStamp { get; set; }
}
