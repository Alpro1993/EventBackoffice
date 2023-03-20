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

namespace EventBackofficeBackend.Repositories;

public class EventsRepository
{
    private readonly EventBackofficeBackendContext _context;

    public EventsRepository(EventBackofficeBackendContext context) 
    {
        _context = context;
    }

    public async Task<PostEventResponse> CreateAsync(PostEventRequest request) 
    {
        if (_context.Events.Any(e => e.Name.Equals(request.Name))) 
        {
            //Commented out to avoid ISE until I find better option
            //throw new InvalidOperationException("An event with that name already exists");
            return new PostEventResponse();
        }

        var @event = new Event {
            Name = request.Name,
            StartDate = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT")),
            EndDate = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", new CultureInfo("pt-PT"))
        };

        await _context.AddAsync(@event);
        await _context.SaveChangesAsync();

        return new PostEventResponse {
            EventID = @event.EventID 
        };
    }

    // public async Task UpdateAsync(int eventId, Event @event)
    // {

    //     if(@event.EventID != eventId) {
    //         throw new InvalidOperationException();
    //     }


    // }

    public async Task DeleteAsync(int id)
    {
        var @event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == id);

        if (@event is null)
        {
            throw new InvalidOperationException("Event was not found");
        }

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
    }

    //GET METHODS
    public async Task<GetEventsResponse> GetEventsAsync(GetEventsRequest request)
    {
        var query = _context.Events.AsQueryable();

        //Check the received request and build the query
        if (request.VenueId > 0)
        {
            query = query.Where(q => q.Venues.Any(x => x.VenueID == request.VenueId));
        }

        if (request.Date is not null)
        {
            var date = DateTime.ParseExact(request.Date, "dd/MM/yyyy", new CultureInfo("pt-PT"));
            query = query.Where(d => d.StartDate.Year == date.Year 
                                                    && d.StartDate.Month == date.Month
                                                    && d.StartDate.Day == date.Day);
        }

        var _events = ProjectToGetEventsResponseDTO(query);

        return new GetEventsResponse {
            Events = await _events
        };
    }

    public async Task<GetSingleEventResponse> GetEventByIdAsync(int id)
    {
        var @event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == id);

        if (@event is not null)
        {
            return new GetSingleEventResponse {
                EventID = @event.EventID,
                Name = @event.Name,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate
            };
        } else 
        {
            // Commented out to stop ISE until I find better option
            //throw new InvalidOperationException("Event not found");
            return new GetSingleEventResponse();
        }

    }


    private Task<List<GetEventsResponse.Event>> ProjectToGetEventsResponseDTO(IQueryable<Event> queryable)
    {
        return queryable.Select(e => new GetEventsResponse.Event
            {
                EventID = e.EventID,
                Name = e.Name,
                StartDate = e.StartDate,
                EndDate = e.EndDate
            }).ToListAsync();
    } 
}
