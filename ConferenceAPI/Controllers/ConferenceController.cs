using ConferenceAPI.Data;
using ConferenceAPI.Models;
using ConferenceAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using Twilio.Rest.Numbers.V2.RegulatoryCompliance;

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
        public ActionResult AddConference([FromBody] ConfLocConfXSpeakerRequest request)
        {
            if (request.StartDate >= request.EndDate)
            {
                return BadRequest("Start date is not valid");
            }
            if (request.StartDate < DateTime.Now)
            {
                return BadRequest("Start date is not valid");
            }      

            if (request.location.CountyId == 0 || request.location.CityId == 0 || request.location.CountyId == 0)
            {
                return BadRequest("Location is not valid");
            }

            if (request.ConferenceTypeId == null)
            {
                return BadRequest("Type or category is not valid");
            }

            var county = _context.DictionaryCounties.Find(request.location.CountyId);
            var city = _context.DictionaryCities.Find(request.location.CityId);
            var country = _context.DictionaryCategories.Find(request.location.CountryId);

            if (county == null)
            {
                return BadRequest("County not found");
            }

            if (city == null)
            {
                return BadRequest("City not found");
            }

            if (string.IsNullOrEmpty(request.location.Address))
            {

                return BadRequest("Address is not valid");
            }

            var speakers = _context.Speakers.Where(s => request.speakers.Select(speaker => speaker.SpeakerId).ToList().Contains(s.Id)).ToList();

            foreach(ConferenceXSpeakerRequest speaker in request.speakers)
            {
                if(!speakers.Any(s=>s.Id == speaker.SpeakerId)){
                    return BadRequest("Speaker doesn't exist");
                }
            }


            Location location = _context.Locations.FirstOrDefault(l => l.Latitude == request.location.Latitude && l.Longitude == request.location.Longitude);

            if(location == null)
            {
                location = new Location
                (
                    request.location.Name,
                    request.location.Code,
                    request.location.CountryId,
                    request.location.Address,
                    request.location.CountyId,
                    request.location.CityId,
                    request.location.Latitude,
                    request.location.Longitude
                );
                _context.Locations.Add(location);
                _context.SaveChanges();
            }

            Conference newConference = new Conference
            {
                Location = location,
                ConferenceTypeId = request.ConferenceTypeId,
                OrganizerEmail = request.OrganizerEmail,
                CategoryId = request.CategoryId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Name = request.conferenceName
            };


           
            foreach(ConferenceXSpeakerRequest speakerRequest in request.speakers)
            {
                ConferenceXspeaker conferenceXspeaker = new ConferenceXspeaker
                {
                    SpeakerId = speakerRequest.SpeakerId,
                    IsMainSpeaker = speakerRequest.isMainSpeaker
                };
                newConference.ConferenceXspeakers.Add(conferenceXspeaker);
            }

            _context.Conferences.Add(newConference);
            _context.SaveChanges();
            return Ok(newConference);
        }

        [HttpPost("attendConference")]
        public ActionResult attendConference([FromBody] ConfXAttendeesRequest request)
        {
             
                var conference = _context.Conferences.Find(request.ConferenceId);
                if (conference == null)
                {
                    return NotFound("Conference not found");
                }

          
                var attendee = _context.ConferenceXattendees.Find();
                if (attendee == null)
                {
                    return NotFound("Attendee not found");
                }

   

                
            }
    }
}
