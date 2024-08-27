using Azure;
using ConferenceAPI.Data;
using ConferenceAPI.Models;
using ConferenceAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
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
                return _context.Speakers.Include(s => s.Feedbacks).Include(s => s.ConferenceXspeakers).ToList();
            }
            else
            {
                return _context.Speakers.Include(s=>s.Feedbacks).Include(s=>s.ConferenceXspeakers).Where(s => s.Name.ToLower().Contains(name.ToLower())).ToList();
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
                return StatusCode(400, "Email format is incorrect");
            }

            string phoneRegexPattern = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";
            if (!Regex.IsMatch(speaker.PhoneNumber, phoneRegexPattern))
            {
                return StatusCode(400, "Phone number format is incorrect");
            }

            var existingSpeaker = _context.Speakers
                .FirstOrDefault(s => s.Email == speaker.Email || s.PhoneNumber == speaker.PhoneNumber);
            if (existingSpeaker != null)
            {
                return StatusCode(400, "A speaker with this email or phone number already exists");
            }


            if (string.IsNullOrEmpty(speaker.Name) || string.IsNullOrEmpty(speaker.Nationality) ||
                string.IsNullOrEmpty(speaker.PhoneNumber) || string.IsNullOrEmpty(speaker.Email))
            {
                return StatusCode(400, "Speaker's fields are empty");
            }

            if (speaker.Rating < 0 || speaker.Rating > 10)
            {
                return StatusCode(400, "The rating is not valid");
            }


            Speaker s = new Speaker(speaker.Name, speaker.Nationality, speaker.Rating, speaker.Image, speaker.PhoneNumber, speaker.Email);
            _context.Speakers.Add(s);
            _context.SaveChanges();

            return StatusCode(201, "Speaker has been added");
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
                return StatusCode(204, "Rating can't be 0");
            }
            existingItem.Rating = Rating;
            _context.SaveChanges();
            return StatusCode(200, "Speaker's rating has been updated");
        }

        [HttpDelete("deleteSpeaker/{id}")]
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

            var speaker = _context.Speakers.Include(s => s.Feedbacks).Include(s => s.ConferenceXspeakers).FirstOrDefault(s => s.Id == id);
            if (speaker == null)
            {
                return NotFound("Speaker not found");
            }

            if (speaker.ConferenceXspeakers.Any())
            {
                _context.ConferenceXspeakers.RemoveRange(speaker.ConferenceXspeakers);
            }

            if (speaker.Feedbacks.Any())
            {
                _context.Feedbacks.RemoveRange(speaker.Feedbacks);
            }


            _context.Speakers.Remove(speaker);
            _context.SaveChanges();

            return StatusCode(204, "Speaker's rows have been delete");
        }
    }
}