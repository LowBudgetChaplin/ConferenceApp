using Azure;
using ConferenceAPI.Data;
using ConferenceAPI.Models;
using ConferenceAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //[HttpGet]
        //public List<Speaker> GetSpeakers(SerbanCorodescuDbContext serbanCorodescuDbContext)
        //{
        //    List<Speaker> speakers = new List<Speaker>();
        //    speakers = serbanCorodescuDbContext.Speakers.ToList();
        //    return speakers;
        //}
        //

        [HttpPost]
        public ActionResult AddSpeaker([FromBody] SpeakerRequest speaker)
        {
            if(speaker == null)
            {
                return BadRequest();
            }
            //if(speaker.Name == null && National)

            Speaker s = new Speaker(speaker.Name, speaker.Nationality, speaker.Rating, speaker.Image, speaker.PhoneNumber, speaker.Email);

            _context.Speakers.Add(s);
            _context.SaveChanges();
            return StatusCode(200, "Speaker has been added!");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSpeaker(int id, [FromBody] SpeakerRequest speaker)
        {
            if (speaker == null)
            {
                return BadRequest();
            }
            Speaker s = new Speaker(speaker.Rating);
            var existingItem = _context.Speakers.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            if(s.Rating == 0)
            {
                return StatusCode(201, "Rating is 0");
            }
            existingItem.Rating = s.Rating;
            _context.SaveChanges();
            return StatusCode(200, "Speaker's rating has been updated!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSpeaker(int id)
        {
            var item = _context.Speakers.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Speakers.Remove(item);
            _context.SaveChanges();
            return StatusCode(204, "Speaker has been deleted!");
        }
    }
}