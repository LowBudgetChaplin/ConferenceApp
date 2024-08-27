using ConferenceAPI.Data;
using ConferenceAPI.Models;
using ConferenceAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ConferenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : Controller
    {
        public SerbanCorodescuDbContext _context { get; set; }
        public FeedbackController(SerbanCorodescuDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddFeedback")]
        public ActionResult AddFeedback([FromBody] FeedbackRequest feedback)
        {
            if (feedback == null)
            {
                return BadRequest();
            }

            Feedback f = new Feedback(feedback.AttendeeEmail, feedback.ConferenceId,
                                      feedback.SpeakerId, feedback.Rating, feedback.Message);

            if (f.AttendeeEmail == null)
            {
                return NotFound("Email not found");
            }

            var conference = _context.Conferences.FirstOrDefault(c => c.Id == feedback.ConferenceId);
            if (conference == null)
            {
                return NotFound("Conference not found");
            }

            var speaker = _context.Speakers.FirstOrDefault(s => s.Id == feedback.SpeakerId);
            if (speaker == null)
            {
                return NotFound("Speaker not found");
            }

            var feedbacks = _context.Feedbacks.Where(f => f.SpeakerId == feedback.SpeakerId).ToList();
            if (speaker.Rating != 0)
            {
                speaker.Rating = feedbacks.Average(f => f.Rating);
            }

            _context.Feedbacks.Add(f);
            _context.SaveChanges();
            return StatusCode(201, "Feedback has been created");
        }

        [HttpGet("GetConference")]
        public ActionResult GetConferencesDesc()
        {
            var conferences = _context.Conferences.Include(c => c.Feedbacks).Select(c => new
            {
                ratingAverage = _context.Feedbacks.Where(f => f.ConferenceId == c.Id).Average(f => f.Rating),
                ratingsCounter = _context.Feedbacks.Count(f => f.ConferenceId == c.Id),


            }).OrderByDescending(c => c.ratingAverage).ToList();

            if (conferences == null)
            {
                return StatusCode(204, "No conference has been found");
            }
            return Ok(conferences);
        }

        [HttpGet("GetAllFeedbacks")]
        public ActionResult getAllConferences(string AttendeeEmail)
        {
            string emailRegexPattern = @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (AttendeeEmail == null || !Regex.IsMatch(AttendeeEmail, emailRegexPattern))
            {
                return BadRequest();
            }

            var feedbacks = _context.Feedbacks.Where(f => f.AttendeeEmail == AttendeeEmail).ToList();


            return Ok(feedbacks);
        }
    }
}