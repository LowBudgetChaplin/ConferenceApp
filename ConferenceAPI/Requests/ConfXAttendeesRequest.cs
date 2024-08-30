namespace ConferenceAPI.Requests
{
    public class ConfXAttendeesRequest
    {
        public int Id { get; set; }

        public string AttendeeEmail { get; set; } = null!;

        public int ConferenceId { get; set; }

        public int StatusId { get; set; }

        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
