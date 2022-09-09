using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GitHubProfiles.Queries.GetListGitHubProfile
{
    public class GetListGitHubProfileQuery:IRequest<GithubProfileListModel>
    {
        public PageRequest PageRequest { get; set; }
    }

    public class GetListGitHubProfileQueryHandler : IRequestHandler<GetListGitHubProfileQuery, GithubProfileListModel>
    {
        private readonly IGitHubProfileRepository _gitHubProfileRepository;
        private readonly IMapper _mapper;

        public GetListGitHubProfileQueryHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper)
        {
            _gitHubProfileRepository = gitHubProfileRepository;
            _mapper = mapper;
        }

        public async Task<GithubProfileListModel> Handle(GetListGitHubProfileQuery request, CancellationToken cancellationToken)
        {
            IPaginate<GitHubProfile> profiles = await _gitHubProfileRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize,
                include: m=>m.Include(m=>m.Developer)
                );
            GithubProfileListModel githubProfileListModel = _mapper.Map<GithubProfileListModel>(profiles);

            return githubProfileListModel;
        }
    }
}
