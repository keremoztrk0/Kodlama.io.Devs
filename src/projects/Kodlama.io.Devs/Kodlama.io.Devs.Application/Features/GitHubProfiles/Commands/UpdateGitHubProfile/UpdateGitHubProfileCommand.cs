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

namespace Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommand:IRequest<UpdatedGitHubProfileDto>
    {
        public int Id { get; set; }
        public string ProfileUrl { get; set; }
    }
    public class UpdateGitHubProfileCommandHandler : IRequestHandler<UpdateGitHubProfileCommand, UpdatedGitHubProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubProfileRepository _gitHubProfileRepository;
        private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

        public UpdateGitHubProfileCommandHandler(IMapper mapper, IGitHubProfileRepository gitHubProfileRepository, GitHubProfileBusinessRules gitHubProfileBusinessRules)
        {
            _mapper = mapper;
            _gitHubProfileRepository = gitHubProfileRepository;
            _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
        }

        public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request, CancellationToken cancellationToken)
        {
            GitHubProfile gitHubProfile = await _gitHubProfileRepository.GetAsync(x => x.Id == request.Id);

            _gitHubProfileBusinessRules.GitHubProfileShouldExistWhenUpdated(gitHubProfile);

            gitHubProfile = await _gitHubProfileRepository.UpdateAsync(gitHubProfile);

            UpdatedGitHubProfileDto updatedGitHubProfileDto = _mapper.Map<UpdatedGitHubProfileDto>(gitHubProfile);

            return updatedGitHubProfileDto;
        }
    }
}
