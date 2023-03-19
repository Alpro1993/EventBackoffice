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
                [FromQuery(Name = "venue")] int venueId = 0, 
                [FromQuery(Name = "date")] string date = null
            )
        {
            //TEMPORARY SOLUTION, WILL BE REPLACED
            if (venueId is 0 && date is null)
            {
                return await repository.GetEventsAsync();
            }
            else if (venueId is not 0 && date is null)
            {
                return await repository.GetEventsByVenue((int)venueId);
            } else if (date is not null)
            {
                return await repository.GetEventsByDate(
                    DateTime.ParseExact(date, "dd/MM/yyyy", new CultureInfo("pt-PT")));
            } else
            {
                throw new Exception("Error getting events");
            }
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

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
    }
}
