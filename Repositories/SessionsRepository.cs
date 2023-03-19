using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Repository;
public class SessionsRepository
{
    private readonly EventBackofficeBackendContext _context;

    public SessionsRepository(EventBackofficeBackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Session session) 
    {
        if (session.SessionID is not 0) 
        {
            throw new InvalidOperationException();
        }

        await _context.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Session>> GetSessionsByEvent(int eventId, bool asNoTracking = false)
    {
        var queryable = _context.Sessions.Where(e => e.EventID == eventId).AsQueryable();
        
        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }
        
        return await queryable.ToListAsync();
    }

    public async Task<Session> GetSessionById(int id, bool asNoTracking = false)
    {
        var queryable = _context.Sessions.AsQueryable();

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().FirstOrDefaultAsync(s => s.SessionID == id);
        }

        return await queryable.FirstOrDefaultAsync(s => s.SessionID == id); 
    }

    public async Task DeleteAsync(int id)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == id);

        if (session is null)
        {
            throw new InvalidOperationException();
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Session>> GetParentlessSessions(int eventId, bool asNoTracking = false)
    {
        var queryable = _context.Sessions.AsQueryable()
                        .Where(e => e.EventID == eventId)
                        .Where(s => s.parentSession == null)
                        .AsQueryable();

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }

        return await queryable.ToListAsync();        
    }

    public async Task<List<Session>> GetChildSessions(int eventId, int sessionId, bool asNoTracking = false)
    {
        var queryable = _context.Sessions
                        .Where(e => e.EventID == eventId)
                        .Where(s => s.parentSession.SessionID == sessionId);

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }

        return await queryable.ToListAsync();
    }

    public async Task<List<Session>> GetChildSessionsByParentId(int id, bool asNoTracking = false)
    {
        var queryable = _context.Sessions.Where(s => s.parentSession.SessionID == id);

            if (asNoTracking)
            {
                return await queryable.AsNoTracking().ToListAsync();
            }

            return await queryable.ToListAsync();
    }

    public async Task<List<Session>> GetSessionsBySponsorId(int eventId, int sponsorId, bool asNoTracking = false)
    {
        var queryable = _context.Sessions
                        .Where(e => e.EventID == eventId)
                        .Where(s => s.Sponsors.Any(i => i.SponsorID == sponsorId));

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }

        return await queryable.ToListAsync();
    }

    public async Task<List<Session>> GetSessionsVenueId(int eventId, int venueId, bool asNoTracking = false) 
    {
        var queryable = _context.Sessions
                        .Where(e => e.EventID == eventId)
                        .Where(s => s.VenueID == venueId);

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }

        return await queryable.ToListAsync();
    }

    public async Task<List<Session>> GetSessionsSpeakerId(int eventId, int speakerId, bool asNoTracking = false)
    {
        var queryable = _context.Sessions
                        .Where(e => e.EventID == eventId)
                        .Where(s => s.Speakers.Any(x => x.PersonID == speakerId));

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }

        return await queryable.ToListAsync();
        
    }

    public async Task<List<Session>> GetSessionsByDate(int eventId, DateTime date, bool asNoTracking = false)
    {
        var queryable = _context.Sessions
        .Where(e => e.EventID == eventId)
        .Where(s => s.StartDate.Year == date.Year
                && s.StartDate.Month == date.Month
                && s.StartDate.Day == date.Day);

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }

        return await queryable.ToListAsync();
    }
}