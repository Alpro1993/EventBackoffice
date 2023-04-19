using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackoffice.Backend.Models.DTOs.Speaker
{
    public class GetMultipleSpeakersResult
    {
        public List<GetSingleSpeakerResult>? Speakers;
    }
}