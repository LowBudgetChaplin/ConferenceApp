namespace ConferenceAPI.Requests
{
    public class WithdrawConferenceRequest
    {
        public int ConferenceId { get; set; }
        public string AttendeeEmail { get; set; } = null!;
        public int WithdrawnStatusId { get; set; }
    }
}
