using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventBackofficeBackend.Models.DTOs.Session;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Repositories;
using AutoMapper;

namespace EventBackofficeBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly EventBackofficeBackendContext _context;
        private readonly IMapper _mapper;
        SessionsRepository repository;

        public SessionsController(EventBackofficeBackendContext context, IMapper mapper)
        {
            _context = context;
            repository = new SessionsRepository {_context = context};
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSingleSessionResponse>> GetSessionById(int id)
        {
            try
            {
                var session =  await repository.GetSessionById(new GetSingleSessionRequest {ID = id});
                return Ok(_mapper.Map<GetSingleSessionResponse>(session));
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

        [HttpGet]
        public async Task<ActionResult<GetMultipleSessionsResponse>> GetSessions
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
            var request = new GetMultipleSessionsRequest
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
            try
            {
                var sessions = await repository.GetSessionsAsync(request);
                var response = new GetMultipleSessionsResponse
                    {
                        Sessions = _mapper.Map<List<GetSingleSessionResponse>>(sessions)
                    };

                return Ok(response);    
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

            try
            {
                var session = await repository.CreateAsync(request);
                return Ok(_mapper.Map<PostSessionResponse>(session));
            }
            catch
            {
                return StatusCode(500);
            }
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

            try
            {
                var session = await repository.PatchAsync(request);
                return Ok(_mapper.Map<GetSingleSessionRequest>(session));
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSession(int id)
        {
            
            try
            {
                await repository.DeleteAsync(id); 
                return Ok();
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
    }
}