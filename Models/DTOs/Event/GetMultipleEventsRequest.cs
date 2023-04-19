using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Event;

[Serializable]
public class GetMultipleEventsRequest
{
    public string? Date {get; set;}
    public int? VenueID {get;set;}       
}
