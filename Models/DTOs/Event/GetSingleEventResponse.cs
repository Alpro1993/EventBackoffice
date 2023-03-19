using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Event;
[Serializable]
public class GetSingleEventResponse
{
    public int EventID {get; set;} = default!;
    public string Name {get; set;} = default!;
    public DateTime StartDate {get; set;} = default!;
    public DateTime EndDate {get; set;} = default!;
}