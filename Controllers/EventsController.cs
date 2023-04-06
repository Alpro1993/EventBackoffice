using Microsoft.AspNetCore.Mvc;
using EventBackofficeBackend.Models.DTOs.Event;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Repositories;
using EventBackofficeBackend.Models;
using AutoMapper;
using EventBackofficeBackend.Mappings.Events;

namespace EventBackofficeBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventBackofficeBackendContext _context;
        EventsRepository repository;
        private readonly IMapper _mapper;

        public EventsController(EventBackofficeBackendContext context, IMapper mapper)
        {
            _context = context;
            repository = new EventsRepository {_context = context};

            _mapper = mapper;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult> GetEvents(int? venueId, string? startDate)
        {
            //Create the request object and validate the parameters
            var parameters = new Parameters {
                ID = venueId,
                StartDate = startDate
            };
            parameters.ValidateParameters();
            
            var request = new GetMultipleEventsRequest {VenueID = venueId!, Date = startDate!};
            
            try 
            {
                var events = await repository.GetEventsAsync(request);
                var response = new GetMultipleEventsResponse{
                    Events = _mapper.Map<List<GetSingleEventResponse>>(events)
                };

                return Ok(response);
            } 
            catch (KeyNotFoundException) 
            {
                return NoContent();
            } 
            catch (FormatException) 
            {
                return BadRequest("Wrong date format - use dd/MM/yyyy");
            }
            catch 
            {
                return StatusCode(500);
            }   
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEventById(int id)
        {
            try
            {
                var @event = await repository.GetEventByIdAsync(id); 
                return Ok(_mapper.Map<GetSingleEventResponse>(@event));
            }
            catch (KeyNotFoundException)
            {
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }

        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostEvent
            (   string Name,
                string StartDate,
                string EndDate)
        {
            var request = new PostEventRequest {
                Name = Name,
                StartDate = StartDate,
                EndDate = EndDate
            };
            
            try 
            {
                var @event = _mapper.Map<PostEventResponse>(await repository.CreateAsync(request));
                return new CreatedAtActionResult("PostEvent", "EventsController", @event.EventID, @event);
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            try
            {
                await repository.DeleteAsync(id);
                return new NoContentResult();
            }
            catch (KeyNotFoundException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        // PATCH: api/Events/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchEvent
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

             try 
             {
             var @event = await repository.PatchAsync(request);
                return new OkObjectResult(_mapper.Map<GetSingleEventResponse>(@event));
             }
             catch
             {
                return StatusCode(500);
             }
        }
    }
}
