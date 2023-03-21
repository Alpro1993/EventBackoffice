using Microsoft.AspNetCore.Mvc;
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
            repository = new EventsRepository {_context = context};
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<GetEventsResponse>> GetEvents
            (
                int venueId = 0, 
                string? startDate = null
            )
        {
            //Create the request object and validate the parameters
            var parameters = new Parameters {
                ID = venueId,
                StartDate = startDate!
            };
            parameters.ValidateParameters();
            var request = new GetEventsRequest {VenueID = venueId, Date = startDate!};
            
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

        // PATCH: api/Events/5
        [HttpPatch("{id}")]
        public async Task<PatchEventResponse> PatchEvent
            (
                int id,
                string? name,
                string? startDate,
                string? endDate
            )
        {
            var parameters = new Parameters
                {
                    ID = id,
                    StartDate = startDate!,
                    EndDate = endDate!
                };
            parameters.ValidateParameters();

            var request = new PatchEventRequest
                {
                    
                    Name = name!,
                    StartDate = startDate!,
                    EndDate = endDate!
                };

            return await repository.PatchAsync(request);
        }
    }
}
