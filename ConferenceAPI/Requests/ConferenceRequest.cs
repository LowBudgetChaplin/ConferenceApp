using ConferenceAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ConferenceAPI.Requests
{
    public class ConferenceRequest
    {
        public int ConferenceTypeId { get; set; }

        public int LocationId { get; set; }

        public string OrganizerEmail { get; set; } = null!;

        public int CategoryId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Name { get; set; } = null!;

        public List<int> SpeakerIds { get; set; }

        public ConferenceRequest() { }


        public ConferenceRequest(int conferenceTypeId, int locationId, string organizerEmail, int categoryId, DateOnly startDate, DateOnly endDate, string name)
        {
            ConferenceTypeId = conferenceTypeId;
            LocationId = locationId;
            OrganizerEmail = organizerEmail;
            CategoryId = categoryId;
            StartDate = startDate;
            EndDate = endDate;
            Name = name;
        }
    }
}