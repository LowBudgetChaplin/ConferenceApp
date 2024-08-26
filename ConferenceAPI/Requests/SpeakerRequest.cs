namespace ConferenceAPI.Requests
{
    public class SpeakerRequest
    {
        public string Name { get; set; } = null!;

        public string? Nationality { get; set; }

        public decimal Rating { get; set; }

        public byte[]? Image { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public SpeakerRequest(string Name, string Nationality,
            decimal Rating, byte[] Image, string PhoneNumber, string Email)
        {
            this.Name = Name;
            this.Nationality = Nationality;
            this.Rating = Rating;
            this.Image = Image;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
        }
    }
}
