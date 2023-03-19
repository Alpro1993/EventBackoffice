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

namespace EventBackofficeBackend.Repositories;

public class EventsRepository
{
    private readonly EventBackofficeBackendContext _context;

    public EventsRepository(EventBackofficeBackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Event @event) 
    {
        if (@event.EventID is not 0) 
        {
            throw new InvalidOperationException();
        }

        await _context.AddAsync(@event);
        await _context.SaveChangesAsync();
    }

    // public async Task UpdateAsync(int eventId, Event @event)
    // {

    //     if(@event.EventID != eventId) {
    //         throw new InvalidOperationException();
    //     }


    // }

    public async Task<GetEventsResponse> GetEventsAsync()
    {
        var queryable = _context.Events.AsQueryable();

        var _events = queryable.Select(e => new GetEventsResponse.Event 
        {
            EventID = e.EventID,
            Name = e.Name,
            StartDate = e.StartDate,
            EndDate = e.EndDate
        }).ToListAsync();

        return new GetEventsResponse {
            events = await _events
        };
    }

    public async Task<Event> GetEventByIdAsync(int id, bool asNoTracking = false)
    {
        var queryable = _context.Events.AsQueryable();

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().FirstOrDefaultAsync(s => s.EventID == id);
        }

        return await queryable.FirstOrDefaultAsync(s => s.EventID == id); 
    }

    public async Task DeleteAsync(int id)
    {
        var @event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == id);

        if (@event is null)
        {
            throw new InvalidOperationException();
        }

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
    }

    // public async Task<List<Event>> GetEventsByVenue(int venueId, bool asNoTracking = false)
    // {
    //     var queryable = _context.Events.Where(s => s.Venues.Any(x => x.VenueID == venueId));

    //     if (asNoTracking)
    //     {
    //         return await queryable.AsNoTracking().ToListAsync();
    //     }

    //     return await queryable.ToListAsync();
    // }

    // public async Task<List<Event>> GetEventsByDate(DateTime date, bool asNoTracking = false)
    // {
    //     var queryable = _context.Events.Where(d => d.StartDate.Year == date.Year 
    //                                                 && d.StartDate.Month == date.Month
    //                                                 && d.StartDate.Day == date.Day);

    //     if (asNoTracking)
    //     {
    //         return await queryable.AsNoTracking().ToListAsync();
    //     }

    //     return await queryable.ToListAsync();
    // } 
}
