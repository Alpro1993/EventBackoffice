using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBackoffice.Backend.Models;
using EventBackoffice.Backend.Models.DTOs.Session;

namespace EventBackoffice.Backend.Mappings
{
    public class SessionMappingProfile : Profile
    {
        public SessionMappingProfile()
        {
            CreateMap<GetSingleSessionRequest, Session>();
            CreateMap<Session, GetSingleSessionResponse>();
            CreateMap<Session, PostSessionResponse>();
        }
    }
}