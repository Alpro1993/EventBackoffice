using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Session
{
    public class GetSessionsRequest
    {
        public int? EventID {get; set;}
        public int? VenueID {get; set;}
        public string? StartDate {get; set;}
        public int? SpeakerID {get; set;}
        public int? ParentID {get; set;}
        public int? SponsorID {get; set;}
        public bool? onlyParentlessSessions {get; set;}
    }
}