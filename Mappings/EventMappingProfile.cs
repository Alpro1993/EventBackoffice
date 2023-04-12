using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBackofficeBackend.Models.DTOs.Event;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Mappings.Events
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