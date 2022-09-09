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

namespace Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.DeleteGithubProfile
{
    public class DeleteGitHubProfileCommand:IRequest<DeletedGitHubProfileDto>
    {
        public int Id { get; set; }
    }
    public class DeletedGitHubProfileCommandHandler : IRequestHandler<DeleteGitHubProfileCommand, DeletedGitHubProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubProfileRepository _gitHubProfileRepository;
        private readonly GitHubProfileBusinessRules _githubProfileBusinessRules;

        public DeletedGitHubProfileCommandHandler(IMapper mapper, IGitHubProfileRepository gitHubProfileRepository, GitHubProfileBusinessRules githubProfileBusinessRules)
        {
            _mapper = mapper;
            _gitHubProfileRepository = gitHubProfileRepository;
            _githubProfileBusinessRules = githubProfileBusinessRules;
        }

        public async Task<DeletedGitHubProfileDto> Handle(DeleteGitHubProfileCommand request, CancellationToken cancellationToken)
        {
            GitHubProfile gitHubProfile = await _gitHubProfileRepository.GetAsync(x => x.Id == request.Id);

            _githubProfileBusinessRules.GitHubProfileShouldExistWhenDeleted(gitHubProfile);

            gitHubProfile = await _gitHubProfileRepository.DeleteAsync(gitHubProfile);

            DeletedGitHubProfileDto deletedGitHubProfileDto = _mapper.Map<DeletedGitHubProfileDto>(gitHubProfile);

            return deletedGitHubProfileDto;
        }
    }
}
