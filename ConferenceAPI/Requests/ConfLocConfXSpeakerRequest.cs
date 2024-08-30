namespace ConferenceAPI.Requests
{
    public class ConfLocConfXSpeakerRequest

    {
            public string? conferenceName { get; set; }

            public LocationRequest location { get; set; } 
            public int ConferenceTypeId { get; set; }

            public string OrganizerEmail { get; set; } = null!;

            public int CategoryId { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public List<ConferenceXSpeakerRequest> speakers { get; set; }


            public ConfLocConfXSpeakerRequest() { }
            public ConfLocConfXSpeakerRequest(string? conferenceName, LocationRequest location, int conferenceTypeId, string organizerEmail, int categoryId, DateTime startDate, DateTime endDate, List<ConferenceXSpeakerRequest> speakers)
            {
                this.conferenceName = conferenceName;
                this.location = location;
                ConferenceTypeId = conferenceTypeId;
                OrganizerEmail = organizerEmail;
                CategoryId = categoryId;
                StartDate = startDate;
                EndDate = endDate;
                this.speakers = speakers;
            }
    }
}
