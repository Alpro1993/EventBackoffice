using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Models;
using EventBackofficeBackend.Models.DTOs.Event;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace EventBackofficeBackend.Repositories;

public class EventsRepository
{
    public required EventBackofficeBackendContext _context;

    // public EventsRepository(EventBackofficeBackendContext context) 
    // {
    //     _context = context;
    // }

    public async Task<ActionResult> CreateAsync(PostEventRequest request) 
    {
        if (_context.Events.Any(e => e.Name.Equals(request.Name))) 
        {
            return new BadRequestObjectResult("An event with that name already exists");
        }

        var @event = new Event {
            Name = request.Name,
            StartDate = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT")),
            EndDate = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", new CultureInfo("pt-PT"))
        };

        await _context.AddAsync(@event);
        await _context.SaveChangesAsync();

        return new CreatedAtActionResult("Create", "EventsController", @event.EventID, @event.EventID);
    }

    public async Task<ActionResult> PatchAsync(PatchEventRequest request)
    {

        var _event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == request.ID);
        
        if (_event is null)
        {
            return new BadRequestObjectResult("EventID was not found");
        }

        // Check what field
        if (request.Name is not null)
        {
            _event.Name = request.Name;
        }

        if (request.StartDate is not null)
        {

            _event.StartDate = DateTime.Parse(request.StartDate);
        }

        if (request.EndDate is not null)
        {
            _event.EndDate = DateTime.Parse(request.EndDate);
        }
        
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(new PatchEventResponse { ID = _event.EventID});
    }

    public async Task<ActionResult> DeleteAsync(int id)
    {
        var @event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == id);

        if (@event is null)
        {
            return new BadRequestObjectResult("Event was not found");
        }

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
        return new NoContentResult();
    }

    //GET METHODS
    public async Task<ActionResult> GetEventsAsync(GetEventsRequest request)
    {
        var query = _context.Events.AsQueryable();

        if (request.VenueID is not null && request.VenueID <= 0)
        {
            return new BadRequestObjectResult("Bad VenueID");
        }

        //Check the received request and build the query
        if (request.VenueID > 0)
        {
            query = query.Where(q => q.Venues.Any(x => x.VenueID == request.VenueID));
        }

        if (request.Date is not null)
        {
            var date = DateTime.ParseExact(request.Date, "dd/MM/yyyy", new CultureInfo("pt-PT"));
            query = query.Where(d => d.StartDate.Year == date.Year 
                                                    && d.StartDate.Month == date.Month
                                                    && d.StartDate.Day == date.Day);
        }

        return new OkObjectResult(await ProjectToGetEventsResponseDTO(query));
    }

    public async Task<ActionResult> GetEventByIdAsync(int id)
    {
        var @event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == id);

        if (@event is not null)
        {
            return new OkObjectResult(new GetSingleEventResponse {
                EventID = @event.EventID,
                Name = @event.Name,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate
            });
        } else 
        {
            return new BadRequestObjectResult("EventID was not found");
        }

    }


    private async Task<GetEventsResponse> ProjectToGetEventsResponseDTO(IQueryable<Event> queryable)
    {
        var _events = await queryable.Select(e => new GetEventsResponse.Event
            {
                EventID = e.EventID,
                Name = e.Name,
                StartDate = e.StartDate,
                EndDate = e.EndDate
            }).ToListAsync();

        return new GetEventsResponse { Events = _events};
    }
}
