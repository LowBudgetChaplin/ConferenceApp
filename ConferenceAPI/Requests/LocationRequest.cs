using Microsoft.AspNetCore.Mvc;

namespace ConferenceAPI.Requests
{
    public class LocationRequest 
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public int CountryId { get; set; }

        public string? Address { get; set; }

        public int CountyId { get; set; }

        public int CityId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
