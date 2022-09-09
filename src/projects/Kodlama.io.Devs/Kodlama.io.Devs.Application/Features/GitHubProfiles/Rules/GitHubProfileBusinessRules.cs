using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GitHubProfiles.Rules
{
    public class GitHubProfileBusinessRules
    {
        private readonly IGitHubProfileRepository _gitHubProfileRepository;

        public GitHubProfileBusinessRules(IGitHubProfileRepository gitHubProfileRepository)
        {
            _gitHubProfileRepository = gitHubProfileRepository;
        }

        public async Task GitHubProfileCanNotBeDuplicatedWhenInserted(int userId,string profileUrl)
        {
            GitHubProfile result = await _gitHubProfileRepository.GetAsync(b => b.DeveloperId == userId && b.ProfileUrl == profileUrl);
            if (result != null) throw new BusinessException("There is already same GitHub profile assigned");
        }

        public void GitHubProfileShouldExistWhenUpdated(GitHubProfile gitHubProfile)
        {
            if (gitHubProfile == null) throw new BusinessException("Requested GitHub profile does not exist");
        }

        public void GitHubProfileShouldExistWhenDeleted(GitHubProfile gitHubProfile)
        {
            if (gitHubProfile == null) throw new BusinessException("Requested GitHub profile does not exist");
        }
    }
}
