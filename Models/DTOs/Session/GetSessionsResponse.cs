using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Session
{
    public class GetSessionsResponse
    {
        public class Session {
            public int SessionID {get; set;} = default!;
            public string Name {get; set;} = default!;
            public DateTime StartDate {get; set;} = default!;
            public DateTime EndDate {get; set;} = default!;
        }

        public List<Session> Sessions {get; set;} = default!;
    }
}