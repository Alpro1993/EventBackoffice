using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Event;
[Serializable]
public class GetSingleEventRequest
{
    public int id {get; set;} = default!;
}
