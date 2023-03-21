using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Session
{
    public class GetSessionsRequest
    {
        public int EventID {get; set;} = default!;
        public int VenueID {get; set;} = default!;
        public string StartDate {get; set;} = default!;
        public int SpeakerID {get; set;} = default;
        public int ParentID {get; set;} = default!;
        public int SponsorID {get; set;} = default;
        public bool onlyParentlessSessions {get; set;} = default!;
    }
}