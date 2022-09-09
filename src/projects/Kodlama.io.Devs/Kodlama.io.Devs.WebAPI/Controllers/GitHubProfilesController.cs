using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.DeleteGithubProfile;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Models;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubProfilesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add(CreateGitHubProfileCommand createGitHubProfileCommand)
        {
            CreatedGitHubProfileDto result = await Mediator.Send(createGitHubProfileCommand);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGitHubProfileCommand updateGitHubProfileCommand)
        {
            UpdatedGitHubProfileDto result = await Mediator.Send(updateGitHubProfileCommand);

            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteGitHubProfileCommand deleteGitHubProfileCommand)
        {
            DeletedGitHubProfileDto result = await Mediator.Send(deleteGitHubProfileCommand);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGitHubProfileQuery getListGitHubProfileQuery = new() { PageRequest = pageRequest };

            GithubProfileListModel result = await Mediator.Send(getListGitHubProfileQuery);

            return Ok(result);
        }
    }
}
