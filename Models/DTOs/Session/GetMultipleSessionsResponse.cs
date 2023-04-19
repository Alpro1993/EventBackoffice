using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Session
{
    public class GetMultipleSessionsResponse
    {
        public List<GetSingleSessionResponse> Sessions {get; set;} = default!;
    }
}