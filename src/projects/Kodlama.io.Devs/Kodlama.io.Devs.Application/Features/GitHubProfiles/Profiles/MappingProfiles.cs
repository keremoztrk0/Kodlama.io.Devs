using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.DeleteGithubProfile;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Models;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GitHubProfiles.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<GitHubProfile, CreateGitHubProfileCommand>().ReverseMap();
            CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();

            CreateMap<GitHubProfile, UpdateGitHubProfileCommand>().ReverseMap();
            CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();

            CreateMap<GitHubProfile, DeleteGitHubProfileCommand>().ReverseMap();
            CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();

            CreateMap<GithubProfileListModel, IPaginate<GitHubProfile>>().ReverseMap();
            CreateMap<GitHubProfile, GitHubProfileListDto>().ForMember(dest=>dest.DeveloperFullName,src=>src.MapFrom(c=>c.Developer.FirstName +" "+ c.Developer.LastName)).ReverseMap();
        }
    }
}
