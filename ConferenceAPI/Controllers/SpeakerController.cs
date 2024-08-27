using Azure;
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
    public class SpeakerController : Controller
    {
        public SerbanCorodescuDbContext _context { get; set; }

        public SpeakerController(SerbanCorodescuDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetSpeakerList")]
        public ActionResult<List<Speaker>> GetSpeakerList(string? name)
        {
            if (name == null)
            {
                return _context.Speakers.ToList();
            }
            else
            {
                return _context.Speakers.Where(s => s.Name.ToLower().Contains(name.ToLower())).ToList();
            }
        }

        [HttpPost]
        public ActionResult AddSpeaker([FromBody] SpeakerRequest speaker)
        {
            if (speaker == null)
            {
                return BadRequest();
            }


            string emailRegexPattern = @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(speaker.Email, emailRegexPattern))
            {
                return StatusCode(400, "Email format incorrect");
            }


            string phoneRegexPattern = @"^\+?[1-9]\d{1,14}$";
            if (!Regex.IsMatch(speaker.PhoneNumber, phoneRegexPattern))
            {
                return StatusCode(400, "Phone number incorrect");
            }


            if (string.IsNullOrEmpty(speaker.Name) || string.IsNullOrEmpty(speaker.Nationality) || speaker.Rating == 0 ||
                string.IsNullOrEmpty(speaker.PhoneNumber) || string.IsNullOrEmpty(speaker.Email))
            {
                return StatusCode(400, "Some of the speaker's fields are empty!");
            }

            Speaker s = new Speaker(speaker.Name, speaker.Nationality, speaker.Rating, speaker.Image, speaker.PhoneNumber, speaker.Email);
            _context.Speakers.Add(s);
            _context.SaveChanges();

            return StatusCode(200, "Speaker has been added");
        }


        [HttpPut("{id}")]
        public ActionResult UpdateSpeaker(int id, [FromBody] int Rating)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var existingItem = _context.Speakers.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            if(Rating == 0)
            {
                return StatusCode(201, "Rating can't be 0");
            }
            existingItem.Rating = Rating;
            _context.SaveChanges();
            return StatusCode(200, "Speaker's rating has been updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSpeaker(int id)
        {
            var speaker = _context.Speakers.FirstOrDefault(i => i.Id == id);
            if(id == 0)
            {
                return StatusCode(409, "Speaker's id can't be 0");
            }
            var isAssignedToConference = _context.ConferenceXspeakers.Any(c => c.Id == id);
            if (isAssignedToConference)
            {
                return StatusCode(209, "Speaker has a conference");
            }

            var hasFeedback = _context.Feedbacks.Any(f => f.SpeakerId == id);
            if (hasFeedback)
            {
                return StatusCode(204, "Speaker has feedback");
            }

            _context.Speakers.Remove(speaker);
            _context.SaveChanges();
            return StatusCode(204, "Speaker has been deleted");
        }


        [HttpGet("GetSpeakerRating/{id}")]
        public ActionResult<object> GetSpeakerRating(int id)
        {
            if (id == 0)
            {
                return StatusCode(400, "Speaker's id can't be 0");
            }

            var speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);

            if (speaker == null)
            {
                return StatusCode(400, "Speaker not found");
            }

            return Ok(speaker.Rating);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSpeakerByConferenceFeedback(int id)
        {
            if (id == 0)
            {
                return BadRequest("Speaker id can't be 0");
            }

            var speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (speaker == null)
            {
                return NotFound("Speaker not found");
            }


            var conferences = _context.ConferenceXspeakers.Where(c => c.SpeakerId == id).ToList();
            var feedbacks = _context.Feedbacks.Where(f => f.SpeakerId == id).ToList();


            if (conferences.Any())
            {
                _context.Conferences.RemoveRange(conferences);
            }

            if (feedbacks.Any())
            {
                _context.Feedbacks.RemoveRange(feedbacks);
            }


            _context.Speakers.Remove(speaker);
            _context.SaveChanges();

            return StatusCode(204, "Speaker's rows have been delete");
        }


    }
}