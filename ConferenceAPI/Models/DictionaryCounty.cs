using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class DictionaryCounty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
