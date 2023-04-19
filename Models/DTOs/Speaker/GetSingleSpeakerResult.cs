using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Speaker
{
    public class GetSingleSpeakerResult
    {
        public int PersonID {get; set;} = default!;
        public string Name {get; set;} = default!;
        public string Picture {get; set;} = default!;
    }
}