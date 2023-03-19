using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Event;
[Serializable]
public class UpdateEventRequest
{
    public string Name {get; set;} = default!;
    public DateTime StartDate {get; set;} = default!;
    public DateTime EndDate {get; set;} = default!;
    public IEnumerable<int> SponsorId {get; set;} = default!;
    public IEnumerable<int> VenueIds {get; set;} = default!;
} 