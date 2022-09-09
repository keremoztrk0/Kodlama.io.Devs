using AutoMapper;
using Core.Security.JWT;
using Kodlama.io.Devs.Application.Features.Developers.Commands.CreateDeveloper;
using Kodlama.io.Devs.Application.Features.Developers.Dtos;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Developers.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Developer, CreateDeveloperCommand>().ReverseMap();
            CreateMap<TokenDto, AccessToken>().ReverseMap();
        }
    }
}
