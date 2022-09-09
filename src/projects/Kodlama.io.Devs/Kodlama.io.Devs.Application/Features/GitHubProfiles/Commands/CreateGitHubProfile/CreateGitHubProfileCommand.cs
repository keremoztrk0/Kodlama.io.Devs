using AutoMapper;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.CreateGitHubProfile
{
    public class CreateGitHubProfileCommand : IRequest<CreatedGitHubProfileDto>
    {
        public int DeveloperId { get; set; }
        public string ProfileUrl { get; set; }


    }

    public class CreateGitHubProfileCommandHandler : IRequestHandler<CreateGitHubProfileCommand, CreatedGitHubProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubProfileRepository _gitHubProfileRepository;
        private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

        public CreateGitHubProfileCommandHandler(IMapper mapper, IGitHubProfileRepository gitHubProfileRepository, GitHubProfileBusinessRules gitHubProfileBusinessRules)
        {
            _mapper = mapper;
            _gitHubProfileRepository = gitHubProfileRepository;
            _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
        }

        public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
        {
            GitHubProfile gitHubProfile = _mapper.Map<GitHubProfile>(request);
            await _gitHubProfileBusinessRules.GitHubProfileCanNotBeDuplicatedWhenInserted(gitHubProfile.DeveloperId, gitHubProfile.ProfileUrl);

            GitHubProfile createdGitHubProfile = await _gitHubProfileRepository.AddAsync(gitHubProfile);

            CreatedGitHubProfileDto createdGitHubProfileDto = _mapper.Map<CreatedGitHubProfileDto>(createdGitHubProfile);

            return createdGitHubProfileDto;

        }
    }
}
