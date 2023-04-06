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

    public async Task<Event> CreateAsync(PostEventRequest request) 
    {
        if (_context.Events.Any(e => e.Name.Equals(request.Name))) 
        {
            throw new InvalidOperationException("An event with that name already exists");
        }

        var @event = new Event {
            Name = request.Name,
            StartDate = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT")),
            EndDate = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", new CultureInfo("pt-PT"))
        };

        await _context.AddAsync(@event);
        await _context.SaveChangesAsync();

        return @event;
    }

    public async Task<Event> PatchAsync(PatchEventRequest request)
    {

        var @event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == request.ID);
        
        if (@event is null)
        {
            throw new KeyNotFoundException("EventID was not found");
        }

        if (request.Name is not null)
        {
            @event.Name = request.Name;
        }

        if (request.StartDate is not null)
        {
            @event.StartDate = DateTime.Parse(request.StartDate);
        }

        if (request.EndDate is not null)
        {
            @event.EndDate = DateTime.Parse(request.EndDate);
        }
        
        await _context.SaveChangesAsync();
        
        return @event;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var @event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == id);

        if (@event is null)
        {
            throw new KeyNotFoundException("Event was not found");
        }

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
        return true;
    }

    //GET METHODS
    public async Task<List<Event>> GetEventsAsync(GetMultipleEventsRequest request)
    {
        var query = _context.Events.AsQueryable();

        if (request.VenueID is int venueID && venueID <= 0)
        {
            throw new ArgumentException("Bad VenueID");
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

        return await query.ToListAsync();
    }

    public async Task<Event> GetEventByIdAsync(int id)
    {
        var @event = await _context.Events.FirstOrDefaultAsync(s => s.EventID == id);

        if (@event is not null)
        {
            return @event;
        } 

        throw new KeyNotFoundException("An Event with the given EventID was not found");
    }
}
