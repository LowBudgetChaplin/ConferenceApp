using ConferenceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceAPI.Requests
{
    public class ConferenceXSpeakerRequest
    {
        public int ConferenceId { get; set; }

        public int SpeakerId { get; set; }

        public bool? IsMainSpeaker { get; set; }


        public string? Name { get; set; }

        public string? Code { get; set; }
        public string? conferenceName { get; set; }

        public string? locationName { get; set; }

        public int CountryId { get; set; }

        public string? Address { get; set; }

        public int CountyId { get; set; }

        public int CityId { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public int ConferenceTypeId { get; set; }

        public string OrganizerEmail { get; set; } = null!;

        public int CategoryId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public List<int> SpeakerIds { get; set; }

    }
}
