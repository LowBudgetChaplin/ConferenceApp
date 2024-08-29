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

            var to = _context.ConferenceXattendees
           .Where(a => a.Id == request.ReceiverId)
           .Select(a => a.AttendeeEmail)
           .FirstOrDefault();
            if(to == null)
            {
                return NotFound("Receiver can't be null");
            }


            var emailNotification = new EmailNotification(
                participantName: attendee.Name,
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
                return StatusCode(400, "Error message: " + ex);
            }
            
        }

        [HttpPost("SendSpeakerEmailNotification")]
        public IActionResult SendSpeakerEmailNotification([FromBody] NotificationRequest request)
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

            var speaker = string.Join(", ", _context.Speakers
            .Where(s => s.Id == request.ConferenceId)
            .Select(s => s.Name));

            var to = _context.ConferenceXattendees
           .Where(a => a.Id == request.ReceiverId)
           .Select(a => a.AttendeeEmail)
           .FirstOrDefault();
            if (to == null)
            {
                return NotFound("Receiver can't be null");
            }

            var mainSpeaker = _context.ConferenceXspeakers
            .Include(cs => cs.Speaker)
            .Where(cs => cs.ConferenceId == request.ConferenceId && cs.IsMainSpeaker == true)
            .Select(cs => new{
                cs.Speaker.Name,
                cs.Speaker.Email
            })
            .FirstOrDefault();

            if (mainSpeaker == null)
            {
                return NotFound("Main speaker not found for the conference");
            }


            var emailNotification = new EmailNotification(
                speakerName: speaker,
                conferenceName: conference.Name,
                date: conference.StartDate,
                location: conference.Location.Address,
                to: mainSpeaker.Email,
                cc: attendee.AttendeeEmail,
                subject: "Conference confirmation"
            );

            try
            {
                _manager.SendNotification(emailNotification);
                _context.EmailNotifications.Add(emailNotification);
                _context.SaveChanges();

                return Ok("Speaker email notification sent");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Error message: " + ex);
            }
        }

        [HttpPost("SendParticipantSmsNotification")]
        public IActionResult SendParticipantSmsNotification([FromBody] NotificationRequest request)
        {
            var attendee = _context.ConferenceXattendees.FirstOrDefault(a => a.Id == request.ReceiverId);
            if (attendee == null)
            {
                return NotFound("Attendee not found");
            }

            var smsNotification = new Smsnotification(
                phoneNumber: "+40757992652",
                message: "Salut!!",
                participantTemplate: "ParticipantTemplate",
                speakerTemplate: string.Empty
            );

            try
            {
                _manager.SendNotification(smsNotification);
                return Ok("Participant SMS notification sent");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Error message: " + ex);
            }
        }

        [HttpPost("SendSpeakerSmsNotification")]
        public IActionResult SendSpeakerSmsNotification([FromBody] NotificationRequest request)
        {
            return Ok("Speaker SMS notification sent");
        }
    }
}
