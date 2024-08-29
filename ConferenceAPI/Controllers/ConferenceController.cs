using ConferenceAPI.Data;
using ConferenceAPI.Models;
using ConferenceAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceController : Controller
    {
        public SerbanCorodescuDbContext _context { get; set; }

        public ConferenceController(SerbanCorodescuDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public ActionResult AddConference([FromBody] ConferenceRequest conferenceRequest, LocationRequest locationRequest, ConferenceXSpeakerRequest conferenceXSpeakerRequest)
        {
            if (conferenceRequest.StartDate >= conferenceRequest.EndDate)
            {
                return BadRequest("Start date is not valid");
            }

            if (locationRequest.CountyId == 0 || locationRequest.CityId == 0 || locationRequest.CountryId == 0)
            {
                return BadRequest("Location is not valid");
            }

            if (conferenceRequest.ConferenceTypeId == null)
            {
                return BadRequest("Type or category is not valid");
            }

            if (conferenceXSpeakerRequest.Address == null)
            {
                return BadRequest("Address is not valid");
            }
            var speakers = _context.Speakers.Where(s => conferenceXSpeakerRequest.SpeakerIds.Contains(s.Id)).ToList();

            if (speakers.Count != conferenceXSpeakerRequest.SpeakerIds.Count)
            {
                return BadRequest("Speaker not found");
            }

            var speakerId = _context.Speakers
                .Where(s => conferenceXSpeakerRequest.SpeakerIds
                .Contains(s.Id))
                .Select(s => s.Id)
                .FirstOrDefault();
            if (speakerId == 0)
            {
                return BadRequest("Speaker not found");
            }


            return Ok();
        }
    }
}
