namespace ConferenceAPI.Requests
{
    public class ConfXAttendeesRequest
    {
        public string AttendeeEmail { get; set; } = null!;

        public int ConferenceId { get; set; }

        public int StatusId { get; set; }
    }
}
