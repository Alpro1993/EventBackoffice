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
        public async Task<ActionResult<GetEventsResponse>> GetEvents()
        {
            return await repository.GetEventsAsync();
        }

        // // GET: api/Events/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Event>> GetEventById(int id)
        // {
        //     return await repository.GetEventByIdAsync(id);
        // }

        // public async Task<ActionResult<List<Event>>> GetEventsByVenueId([FromQuery(Name = "venue")] int venueId)
        // {
        //     return await repository.GetEventsByVenue(venueId);
        // }

        // public async Task<ActionResult<List<Event>>> GetEventsByDate([FromQuery(Name = "date")] DateTime date)
        // {
        //     return await repository.GetEventsByDate(date);
        // }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPatch("{id}")]
        // public async Task<IActionResult> PatchEvent(int id, Event @event)
        // {
        //     if (id != @event.EventID)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(@event).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!EventExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Events
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Event>> PostEvent(Event @event)
        // {
        //     _context.Events.Add(@event);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetEvent", new { id = @event.EventID }, @event);
        // }

        // // DELETE: api/Events/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteEvent(int id)
        // {
        //     var @event = await _context.Events.FindAsync(id);
        //     if (@event == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Events.Remove(@event);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool EventExists(int id)
        // {
        //     return _context.Events.Any(e => e.EventID == id);
        // }
    }
}
