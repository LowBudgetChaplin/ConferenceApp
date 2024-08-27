using Azure.Core;
using ConferenceAPI.Data;
using ConferenceAPI.Requests;
using ConferenceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        public SerbanCorodescuDbContext _context { get; set; }
        public NotificationManager _manager { get; set; }

        public NotificationController(SerbanCorodescuDbContext context, NotificationManager manager)
        {
            _context = context;
            _manager = manager;
        }

        [HttpPost("SendParticipantEmailNotification")]
        public IActionResult SendParticipantEmailNotification([FromBody] NotificationRequest request)
        {

            return Ok("Participant email notification sent.");
        }

        [HttpPost("SendSpeakerEmailNotification")]
        public IActionResult SendSpeakerEmailNotification([FromBody] NotificationRequest request)
        {

            return Ok("Speaker email notification sent.");
        }

        [HttpPost("SendParticipantSmsNotification")]
        public IActionResult SendParticipantSmsNotification([FromBody] NotificationRequest request)
        {
            return Ok("Participant SMS notification sent.");
        }

        [HttpPost("SendSpeakerSmsNotification")]
        public IActionResult SendSpeakerSmsNotification([FromBody] NotificationRequest request)
        {
            return Ok("Speaker SMS notification sent.");
        }

    }
}
