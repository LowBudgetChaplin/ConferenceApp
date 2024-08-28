using ConferenceAPI.Data;
using ConferenceAPI.Models;
using ConferenceAPI.Requests;
using ConferenceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConferenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly SerbanCorodescuDbContext _context;
        private readonly NotificationManager _manager;
        private readonly EmailService _emailService;

        public NotificationController(SerbanCorodescuDbContext context)
        {
            _context = context;
            _manager = new NotificationManager();
        }

        [HttpPost("SendParticipantEmailNotification")]
        public IActionResult SendParticipantEmailNotification([FromBody] NotificationRequest request)
        {
            var conference = _context.Conferences.Include(c => c.Location).FirstOrDefault(c => c.Id == request.ConferenceId);
            if (conference == null)
            {
                return NotFound("Conference not found");
            }

            var attendee = _context.ConferenceXattendees.FirstOrDefault(a => a.Id == request.ReceiverId);
            if (attendee == null)
            {
                return NotFound("Attendee not found");
            }

             var speakers = string.Join(", ", _context.Speakers
            .Where(s => s.Id == request.ConferenceId)
            .Select(s => s.Name));

            // var to = _context.ConferenceXattendees
            //.Where(a => a.Id == request.ReceiverId)
            //.Select(a => a.AttendeeEmail)
            //.FirstOrDefault();



            var emailNotification = new EmailNotification(
                attendeeName: attendee.Name,
                conferenceName: conference.Name,
                speakerNames: speakers,
                date: conference.StartDate,
                location: conference.Location.Address,
                to: attendee.AttendeeEmail,
                cc: "",
                subject: "Do not respond"
            );

            try
            {
                _manager.SendNotification(emailNotification);
                _context.EmailNotifications.Add(emailNotification);
                _context.SaveChanges();

                return Ok("Participant email notification sent");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Error");
            }
            
        }

        [HttpPost("SendSpeakerEmailNotification")]
        public IActionResult SendSpeakerEmailNotification([FromBody] NotificationRequest request)
        {
            return Ok("Email sent");
        }

        [HttpPost("SendParticipantSmsNotification")]
        public IActionResult SendParticipantSmsNotification([FromBody] NotificationRequest request)
        {
            return Ok("Participant SMS notification sent");
        }

        [HttpPost("SendSpeakerSmsNotification")]
        public IActionResult SendSpeakerSmsNotification([FromBody] NotificationRequest request)
        {
            return Ok("Speaker SMS notification sent");
        }
    }
}
