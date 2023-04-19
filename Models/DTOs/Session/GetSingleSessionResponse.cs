using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Session
{
    public class GetSingleSessionResponse
    {
        public int SessionID {get; set;} = default!;
        public string Name {get; set;} = default!;
        public DateTime StartDate {get; set;} = default!;
        public DateTime EndDate {get; set;} = default!;
    }
}