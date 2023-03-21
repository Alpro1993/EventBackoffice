using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Models;
using EventBackofficeBackend.Models.DTOs.Session;
using EventBackofficeBackend.Repositories.ExtensionMethods;

using System.Globalization;


namespace EventBackofficeBackend.Repositories;

public class SessionsRepository
{
    public required EventBackofficeBackendContext _context;

    public SessionsRepository() {}

    public async Task<PostSessionResponse> CreateAsync(PostSessionRequest request) 
    {
        Session _session = new Session
            {
                Name = request.Name,
                StartDate = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT")),
                EndDate = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", new CultureInfo("pt-PT"))
            };

        await _context.AddAsync(_session);
        await _context.SaveChangesAsync();

        return new PostSessionResponse { ID = _session.SessionID};
    }

    public async Task<PatchSessionResponse> PatchAsync(PatchSessionRequest request)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == request.ID);

        if (session is null)
        {
            throw new ArgumentException("No session found with ID " + request.ID);
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

        return new PatchSessionResponse { ID = session.SessionID };
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


    public async Task<GetSingleSessionResponse> GetSessionById(GetSingleSessionRequest request)
    {
        var _session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == request.ID);

        if (_session is null)
        {
            throw new ArgumentException("No session found with ID " + request.ID);
        }
        return new GetSingleSessionResponse {
            ID = _session.SessionID,
            Name = _session.Name,
            StartDate = _session.StartDate
        }; 
    }

    public async Task<GetSessionsResponse> GetSessionsAsync(GetSessionsRequest request)
    {
        var query = _context.Sessions.AsQueryable();

        //Check the received request and build the query
        if (request.EventID > 0)
        {
            query = query.Where(e => e.EventID == request.EventID);
        }

        if (request.SpeakerID > 0)
        {
            query = query.Where(s => s.Speakers.Any(x => x.PersonID == request.SpeakerID));
        }

        if (request.VenueID > 0)
        {
            // query = query.QuerySessionsByVenueId(request.VenueID);
            query = query.Where(s => s.VenueID == request.VenueID);
        }
        if (request.StartDate is not null)
        {
            var date = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT"));
            query = query.Where(d => d.StartDate.Year == date.Year 
                                                    && d.StartDate.Month == date.Month
                                                    && d.StartDate.Day == date.Day);
        }

        if (request.ParentID > 0)
        {
            query = query.Where(s => s.parentSessionID == request.ParentID);
        }

        if (request.onlyParentlessSessions is true)
        {
            query = query.Where(s => s.parentSessionID > 0);
        }

        if (request.SponsorID > 0)
        {
            query = query.Where(s => s.Sponsors.Any(i => i.SponsorID == request.SponsorID));
        }

        return await ProjectToGetSessionsResponseDTO(query);
    }

    private async Task<GetSessionsResponse> ProjectToGetSessionsResponseDTO(IQueryable<Session> queryable)
    {
            var _sessions = await queryable.Select(s => new GetSessionsResponse.Session
            {
                SessionID = s.SessionID,
                Name = s.Name,
                StartDate = s.StartDate,
            }).ToListAsync();

        return new GetSessionsResponse { Sessions = _sessions};
    }
}