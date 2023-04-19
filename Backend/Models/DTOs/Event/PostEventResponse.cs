using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Event
{
    public class PostEventResponse
    {
    public int EventID {get; set;} = default!;
    public string Name {get; set;} = default!;
    public DateTime StartDate {get; set;} = default!;
    public DateTime EndDate {get; set;} = default!;

    }
}