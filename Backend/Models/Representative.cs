using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models
{
    public class Representative : Person
    {
        public int RepresentativeID {get; set;}
        public Sponsor? Sponsor {get; set;}
        public Event? Event {get; set;}

    }
}