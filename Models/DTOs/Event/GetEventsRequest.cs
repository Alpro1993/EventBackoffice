using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Event;

[Serializable]
public class GetEventsRequest
{
    public string Date {get; set;} = default!;
    public int VenueID {get;set;} = default!;       
}
