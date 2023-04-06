using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Models;
using EventBackofficeBackend.Models.DTOs.Session;
using EventBackofficeBackend.Repositories.ExtensionMethods;

using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace EventBackofficeBackend.Repositories;

public class SessionsRepository
{
    public required EventBackofficeBackendContext _context;

    public SessionsRepository() {}

    public async Task<ActionResult> CreateAsync(PostSessionRequest request) 
    {
        Session _session = new Session
            {
                Name = request.Name,
                StartDate = DateTime.ParseExact(request.StartDate, "dd/MM/yyyy", new CultureInfo("pt-PT")),
                EndDate = DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", new CultureInfo("pt-PT"))
            };

        await _context.AddAsync(_session);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new PostSessionResponse { ID = _session.SessionID});
    }

    public async Task<ActionResult> PatchAsync(PatchSessionRequest request)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == request.ID);

        if (session is null)
        {
            return new BadRequestResult();
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

        return new OkObjectResult(new PatchSessionResponse { ID = session.SessionID });
    }

    public async Task<ActionResult> DeleteAsync(int id)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == id);

        if (session is null)
        {
            return new BadRequestResult();
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
        return new NoContentResult();
    }


    public async Task<ActionResult> GetSessionById(GetSingleSessionRequest request)
    {
        var _session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionID == request.ID);

        if (_session is null)
        {
            return new EmptyResult();
        }
        return new OkObjectResult(new GetSingleSessionResponse 
            {
                ID = _session.SessionID,
                Name = _session.Name,
                StartDate = _session.StartDate
            }); 
    }

    public async Task<ActionResult> GetSessionsAsync(GetSessionsRequest request)
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

        return new OkObjectResult(await ProjectToGetSessionsResponseDTO(query));
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