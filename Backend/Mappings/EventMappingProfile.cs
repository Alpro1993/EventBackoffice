using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBackoffice.Backend.Models.DTOs.Event;
using EventBackoffice.Backend.Models;

namespace EventBackoffice.Backend.Mappings
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<GetSingleEventRequest, Event>();
            CreateMap<Event, GetSingleEventResponse>();
            CreateMap<Event, PostEventResponse>();
        }
    }
}