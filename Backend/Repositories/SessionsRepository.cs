using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using EventBackoffice.Backend.Data;
using EventBackoffice.Backend.Models;
using EventBackoffice.Backend.Models.DTOs.Session;
using EventBackoffice.Backend.Repositories.ExtensionMethods;

using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace EventBackoffice.Backend.Repositories;

public class SessionsRepository
{
    public required BackendContext _context;

    public SessionsRepository() {}

    public async Task<Session> CreateAsync(PostSessionRequest request) 
    {
        Session session = new Session
            {
                Name = request.Name,
                StartDate = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT")),
                EndDate = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", new CultureInfo("pt-PT"))
            };

        await _context.AddAsync(session);
        await _context.SaveChangesAsync();

        return session;
    }

    public async Task<Session> PatchAsync(PatchSessionRequest request)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == request.ID);

        if (session is null)
        {
            throw new KeyNotFoundException("A session with the given ID was not found");
        }
        if (request.Name is not null)
        {
            session.Name = request.Name;
        }
        if (request.StartDate is not null)
        {
            session.StartDate = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT"));
        }
        if (request.EndDate is not null)
        {
            session.EndDate = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", new CultureInfo("pt-PT"));
        }
        
        await _context.SaveChangesAsync();

        return session;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == id);

        if (session is null)
        {
            throw new KeyNotFoundException("A session with the given ID was not found");
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<Session> GetSessionById(GetSingleSessionRequest request)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == request.ID);

        if (session is null)
        {
            throw new KeyNotFoundException("A session with the given ID was not found");
        }
        return session;
    }

    public async Task<List<Session>> GetSessionsAsync(GetMultipleSessionsRequest request)
    {
        var query = _context.Sessions.AsQueryable();

        //Check the received request and build the query
        if (request.EventID is int eventID && eventID > 0)
        {
            query = query.Where(e => e.EventID == eventID);
        }

        if (request.SpeakerID is int speakerID && speakerID > 0)
        {
            query = query.QuerySessionsBySpeakerID(speakerID);
        }

        if (request.VenueID is int venueID && venueID > 0)
        {
            query = query.QuerySessionsByVenueID(venueID);
        }

        if (request.StartDate is not null)
        {
            var date = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT"));
            query = query.QuerySessionsByDate(date);
        }

        if (request.ParentID is int parentID && parentID> 0)
        {
            query = query.QueryChildSessionsBySessionID(parentID);
        }

        if (request.onlyParentlessSessions is true)
        {
            query = query.QueryParentlessSessions();
        }

        if (request.SponsorID is int sponsorID && sponsorID > 0)
        {
            query = query.QuerySessionsBySponsorID(sponsorID);
        }

        return await query.ToListAsync();
    }
}