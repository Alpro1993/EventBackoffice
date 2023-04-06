using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Models.DTOs.Speaker
{
    public class PostSpeakerRequest
    {
        public int EventID {get; set;} = default!;
        public string Name {get; set;} = default!;
    }
}