using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Models.DTOs.Session
{
    public class PatchSessionRequest
    {
        public int ID {get; set;} = default!;
        public string Name {get; set;} = default!;
        public string StartDate {get; set;} = default!;
        public string EndDate {get; set;} = default!;
    }
}