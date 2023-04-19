using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Speaker
{
    public class GetSingleSpeakerRequest
    {
        public int SpeakerID {get; set;}
        public string? Name {get; set;}
        public string? Picture {get; set;}
    }
}