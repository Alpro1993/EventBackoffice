using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBackofficeBackend.Models;
using EventBackofficeBackend.Models.DTOs.Session;

namespace EventBackofficeBackend.Mappings
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