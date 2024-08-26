using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class DictionaryCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<Conference> Conferences { get; set; } = new List<Conference>();
}
