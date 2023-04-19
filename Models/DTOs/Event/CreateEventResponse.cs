using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Event;

[Serializable]
public class CreateEventResponse
{
    public int EventID {get; set;} = default!;
}
