using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Repositories.ExtensionMethods
{

    public static class SessionQueries
    {
        public static IQueryable<Session> QuerySessionsByEvent(IQueryable<Session> _query, int eventId)
        {
            var query = _query.Where(e => e.EventID == eventId);
            
            return query;
        }

        public static IQueryable<Session> QueryParentlessSessions(IQueryable<Session> _query)
        {
            var query = _query.Where(s => s.parentSession == null);

            return query;        
        }

        public static IQueryable<Session> QueryChildSessionsBySessionId(IQueryable<Session> _query, int sessionId)
        {
            var query = _query.Where(s => s.parentSessionID == sessionId);

            return query;
        }

        public static IQueryable<Session> QuerySessionsBySponsorId(IQueryable<Session> _query, int sponsorId)
        {
            var query = _query.Where(s => s.Sponsors.Any(i => i.SponsorID == sponsorId));

            return query;
        }

        public static IQueryable<Session> QuerySessionsByVenueId(IQueryable<Session> _query, int venueId) 
        {
            var query = _query.Where(s => s.VenueID == venueId);

            return query;
        }

        public static IQueryable<Session> QuerySessionsBySpeakerId(IQueryable<Session> _query, int speakerId)
        {
            var query = _query.Where(s => s.Speakers.Any(x => x.PersonID == speakerId));

            return query;
        }

        public static IQueryable<Session> QuerySessionsByDate(IQueryable<Session> _query, DateTime date)
        {
            var query = _query.Where(s => s.StartDate.Year == date.Year
                                && s.StartDate.Month == date.Month
                                && s.StartDate.Day == date.Day);

            return query;
        }
    }
}