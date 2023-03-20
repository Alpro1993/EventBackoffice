using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventBackofficeBackend.Models.DTOs.Event;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Repositories;
using System.Globalization;

namespace EventBackofficeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventBackofficeBackendContext _context;
        EventsRepository repository;

        public EventsController(EventBackofficeBackendContext context)
        {
            _context = context;
            repository = new EventsRepository(_context);
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<GetEventsResponse>> GetEvents
            (
                [FromQuery(Name = "venue")] int _venueId = 0, 
                [FromQuery(Name = "date")] string _date = null
            )
        {
            //Create the request object and validate the parameters
            ValidateParameters(_venueId, _date);
            var request = new GetEventsRequest {VenueId = _venueId, Date = _date};
            
            //Pass the request object to the repository and retrieve the response object.
            return await repository.GetEventsAsync(request);        
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetSingleEventResponse>> GetEventById(int id)
        {
            return await repository.GetEventByIdAsync(id);
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostEventResponse>> PostEvent
            (   string Name,
                string StartDate,
                string EndDate)
        {
            var request = new PostEventRequest {
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate
            };

            return await repository.CreateAsync(request);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await repository.DeleteAsync(id);

            return NoContent();
        }

        //AUXILIARY METHODS
        private void ValidateParameters(int _venueId, string _date)
        {
            if (_venueId <= 0) 
            {
                throw new ArgumentOutOfRangeException("venueId", "Venue Id must be greater than zero");
            }

            if (_date is not null)
            {
                try
                {
                    DateTime.ParseExact(_date, "dd/MM/yyyy", new CultureInfo("pt-PT"));
                }
                catch (FormatException e)
                {
                    throw new ArgumentException(e.GetBaseException().ToString());
                }
            }
        }

    }
}
