using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Event;

[Serializable]
public class GetEventsRequest
{
    public DateTime date {get; set;} = default!;
    public Venue venue {get;set;} = default!;       
}
