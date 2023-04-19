using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Session
{
    public class PostSessionRequest
    {
        public string Name {get; set;} = default!;
        public string StartDate {get; set;} = default!;
        public string EndDate {get; set;} = default!;
    }
}