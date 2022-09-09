using Kodlama.io.Devs.Application.Features.Developers.Commands.CreateDeveloper;
using Kodlama.io.Devs.Application.Features.Developers.Commands.LoginDeveloper;
using Kodlama.io.Devs.Application.Features.Developers.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]CreateDeveloperCommand createDeveloperCommand)
        {
            TokenDto result = await Mediator.Send(createDeveloperCommand);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDeveloperCommand loginDeveloperCommand)
        {
            TokenDto result = await Mediator.Send(loginDeveloperCommand);

            return Ok(result);
        }
    }
}
