using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Repositories.ExtensionMethods
{

    public static class SessionQueries
    {
        public static IQueryable<Session> QuerySessionsByEvent(this IQueryable<Session> _query, int eventId)
        {
            return _query.Where(e => e.EventID == eventId);
        }

        public static IQueryable<Session> QueryParentlessSessions(this IQueryable<Session> _query)
        {
            return _query.Where(s => s.parentSession == null);
        }

        public static IQueryable<Session> QueryChildSessionsBySessionID(this IQueryable<Session> _query, int sessionId)
        {
            return _query.Where(s => s.parentSessionID == sessionId);
        }

        public static IQueryable<Session> QuerySessionsBySponsorID(this IQueryable<Session> _query, int sponsorId)
        {
            return _query.Where(s => s.Sponsors.Any(i => i.SponsorID == sponsorId));
        }

        public static IQueryable<Session> QuerySessionsByVenueID(this IQueryable<Session> _query, int venueId) 
        {
            return _query.Where(s => s.VenueID == venueId);
        }

        public static IQueryable<Session> QuerySessionsBySpeakerID(this IQueryable<Session> _query, int speakerId)
        {
            return _query.Where(s => s.Speakers.Any(x => x.PersonID == speakerId));
        }

        public static IQueryable<Session> QuerySessionsByDate(this IQueryable<Session> _query, DateTime date)
        {
            return _query.Where(s => s.StartDate.Year == date.Year
                                && s.StartDate.Month == date.Month
                                && s.StartDate.Day == date.Day);
        }
    }
}