using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBackoffice.Backend.Models;

namespace EventBackoffice.Backend.Repositories.ExtensionMethods
{
    public static class SpeakerQueries
    {
        public static IQueryable<Speaker> QuerySpeakersByEvent(this IQueryable<Speaker> _query, int eventId)
        {
            return _query.Where(e => e.Event.EventID == eventId);
        }
    }
}