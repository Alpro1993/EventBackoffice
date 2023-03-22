using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventBackofficeBackend.Models.DTOs.Session;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Repositories;

namespace EventBackofficeBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly EventBackofficeBackendContext _context;
        SessionsRepository repository;

        public SessionsController(EventBackofficeBackendContext context)
        {
            _context = context;
            repository = new SessionsRepository {_context = context};
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSingleSessionResponse>> GetSingleSession(int id)
        {
            return await repository.GetSessionById(new GetSingleSessionRequest {ID = id});
        }

        [HttpGet]
        public async Task<ActionResult<GetSessionsResponse>> GetSessions
            (
                string? StartDate = null,
                int EventId = 0,
                int SpeakerId = 0,
                int VenueId = 0,
                int ParentId = 0,
                int SponsorId = 0,
                bool Parentless = true
            )
        {
            //Creates a dictionary of IDs for parameter checking.
            //This allows better reporting of errors.
            var IDs = new Dictionary<string, int>()
            {
                {"EventId", EventId},
                {"SpeakerId", SpeakerId},
                {"VenueId", VenueId},
                {"ParentId", ParentId},
                {"SponsorId", SponsorId}
            };

            //Constructs a Parameters object for validation and validates all parameters.
            var parameters = new Parameters 
            {
                IDs = IDs,
                StartDate = StartDate!
            };
            parameters.ValidateParameters();

            //Create a request object to pass to the repository
            var request = new GetSessionsRequest
                            {
                                EventID = EventId,
                                SpeakerID = SpeakerId,
                                VenueID = VenueId,
                                ParentID = ParentId,
                                SponsorID = SponsorId,
                                onlyParentlessSessions = Parentless,
                                StartDate = StartDate!
                            };
            
            //Pass the request object and retrieve the response object.
            return await repository.GetSessionsAsync(request);
        }

        [HttpPost]
        public async Task<ActionResult<PostSessionResponse>> PostSession
            (
                string Name,
                string StartDate,
                string EndDate
            )
        {
            var request = new PostSessionRequest
                {
                    Name = Name,
                    StartDate = StartDate,
                    EndDate = EndDate
                };

            return await repository.CreateAsync(request);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchSession
            (
                int ID,
                string? Name,
                string? StartDate,
                string? EndDate
            )
        {
            var parameters = new Parameters
                {
                    ID = ID,
                    StartDate = StartDate!,
                    EndDate = EndDate!
                };
            parameters.ValidateParameters();

            var request = new PatchSessionRequest
                {
                    ID = ID,
                    Name = Name!,
                    StartDate = StartDate!,
                    EndDate = EndDate!
                };

            return await repository.PatchAsync(request);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSession(int id)
        {
            
            await repository.DeleteAsync(id);

            return NoContent();
        }
    }
}