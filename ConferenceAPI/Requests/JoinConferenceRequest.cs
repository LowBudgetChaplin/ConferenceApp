namespace ConferenceAPI.Requests
{
    public class JoinConferenceRequest
    {
            public int ConferenceId { get; set; }
            public string AttendeeEmail { get; set; } = null;
            public int joinedStatusId { get; set; }
    }

}

