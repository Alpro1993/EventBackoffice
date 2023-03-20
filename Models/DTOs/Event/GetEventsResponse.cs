using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Event;
[Serializable]
public class GetEventsResponse
{
    public class Event
    {
        public int EventID {get; set;} = default!;
        public string Name {get; set;} = default!;
        public DateTime StartDate {get; set;} = default!;
        public DateTime EndDate {get; set;} = default!;
    }
    public List<Event> Events {get; set;} = default!;
}