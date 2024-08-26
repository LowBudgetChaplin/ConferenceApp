using System;
using System.Collections.Generic;

namespace ConferenceAPI.Models;

public partial class Location
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public int CountryId { get; set; }

    public string? Address { get; set; }

    public int CountyId { get; set; }

    public int CityId { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual DictionaryCity City { get; set; } = null!;

    public virtual ICollection<Conference> Conferences { get; set; } = new List<Conference>();

    public virtual DictionaryCountry Country { get; set; } = null!;

    public virtual DictionaryCounty County { get; set; } = null!;
}
