using ConferenceAPI.Data;
using ConferenceAPI.Models;
using ConferenceAPI.Requests;
using ConferenceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Twilio.TwiML.Messaging;
using Twilio.TwiML.Voice;

namespace ConferenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly SerbanCorodescuDbContext _context;
        private readonly NotificationManager _manager;
        //private readonly EmailService _emailService;
        //private readonly SmsService _smsService;

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

            if (conference.Location.Address == null)
            {
                return NotFound("Address is not specified");
            }

            var attendee = _context.ConferenceXattendees.FirstOrDefault(a => a.Id == request.ReceiverId);
            if (attendee == null)
            {
                return NotFound("Attendee not found");
            }



            var speakerNames = string.Join(", ", _context.ConferenceXspeakers
            .Where(s => s.ConferenceId == request.ConferenceId)
            .Select(s => s.Speaker.Name));

            Speaker mainSpeaker = _context.ConferenceXspeakers
            .Where(s => s.IsMainSpeaker == true && request.ConferenceId == s.ConferenceId)
            .Select(s => s.Speaker)
            .FirstOrDefault();

            if (mainSpeaker == null)
            {
                mainSpeaker = _context.ConferenceXspeakers
                .Where(s=> s.ConferenceId == request.ConferenceId)
                .Select(s => s.Speaker)
                .FirstOrDefault();
            }

            var to = _context.ConferenceXattendees
           .Where(a => a.Id == request.ReceiverId && a.ConferenceId == request.ConferenceId)
           .Select(a => a.AttendeeEmail)
           .FirstOrDefault();
            if(to == null)
            {
                return NotFound("Receiver is not assigned to this conference");
            }


            var emailNotification = new EmailNotification(
                participantName: attendee.Name,
                conferenceName: conference.Name,
                speakerNames: speakerNames,
                date: conference.StartDate,
                location: conference.Location.Address,
                to: to,
                cc: mainSpeaker.Email,
                subject: "You have been invited to the conference"
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
            var conference = _context.Conferences.Include(c => c.Location)
                .FirstOrDefault(c => c.Id == request.ConferenceId);
            if (conference == null)
            {
                return NotFound("Conference not found");
            }
            if (string.IsNullOrEmpty(conference.Location.Address))
            {
                return NotFound("Address is not specified");
            }

            var attendee = _context.ConferenceXattendees
                .FirstOrDefault(a => a.Id == request.ReceiverId);
            if (attendee == null)
            {
                return NotFound("Attendee not found");
            }

            var speakerNames = string.Join(", ", _context.ConferenceXspeakers
                .Where(s => s.ConferenceId == request.ConferenceId)
                .Select(s => s.Speaker.Name));

            var speaker = _context.ConferenceXspeakers
                .Include(cs => cs.Speaker)
                .Where(cs => cs.ConferenceId == request.ConferenceId && cs.IsMainSpeaker == true)
                .Select(cs => new { cs.Speaker.Name, cs.Speaker.Email })
                .FirstOrDefault();

            if (speaker == null)
            {
                return NotFound("Main speaker not found for the specified conference");
            }

            var emailNotification = new EmailNotification(
                speakerName: speaker.Name,
                conferenceName: conference.Name,
                date: conference.StartDate,
                location: conference.Location.Address,
                to: speaker.Email,
                cc: attendee.AttendeeEmail,
                subject: "You have been assigned as speaker for the conference"
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
                return StatusCode(500, "Error message: " + ex.Message);
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

            var phoneNumber = attendee.PhoneNumber;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Attendee's phone number is missing");
            }

            var smsNotification = new Smsnotification(
                phoneNumber: phoneNumber,
                message: "You have been invited to the conference"
            );

            try
            {
                _manager.SendNotification(smsNotification);
                _context.Smsnotifications.Add(smsNotification);
                smsNotification.SentDate = DateTime.Now;
                _context.SaveChanges();
                return Ok("Participant SMS notification sent");
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Error message: {ex.Message}");
            }
        }


        [HttpPost("SendSpeakerSmsNotification")]
        public IActionResult SendSpeakerSmsNotification([FromBody] NotificationRequest request)
        {
            var conference = _context.Conferences.Include(c => c.Location).FirstOrDefault(c => c.Id == request.ConferenceId);
            if (conference == null)
            {
                return NotFound("Conference not found");
            }

            var speaker = _context.ConferenceXspeakers
                .Include(cs => cs.Speaker)
                .Where(cs => cs.ConferenceId == request.ConferenceId)
                .Select(cs => cs.Speaker)
                .FirstOrDefault();

            var phoneNumber = speaker.PhoneNumber;

            var smsNotification = new Smsnotification(
                phoneNumber: phoneNumber,
                message: $"You have been assigned as speaker for the conference: {conference.Name}"
            );

            var smsService = new SmsService();


            try
            {
                _manager.SendNotification(smsNotification);
                smsNotification.SentDate = DateTime.Now;
                _context.Smsnotifications.Add(smsNotification);
                _context.SaveChanges();
                return Ok("Speaker SMS notification sent");
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Error message: " + ex);
            }
        }
    }
}
