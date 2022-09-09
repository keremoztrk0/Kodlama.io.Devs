using Kodlama.io.Devs.Application.Features.GitHubProfiles.Dtos;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GitHubProfiles.Models
{
    public class GithubProfileListModel
    {
        public ICollection<GitHubProfileListDto> Items { get; set; }
    }
}
