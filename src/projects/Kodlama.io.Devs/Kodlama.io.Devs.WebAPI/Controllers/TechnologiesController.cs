using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Technologies.Models;
using Kodlama.io.Devs.Application.Features.Technologies.Queries.GetListTechnology;
using Kodlama.io.Devs.Application.Features.Technologies.Commands.UpdateTechnology;
using Kodlama.io.Devs.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kodlama.io.Devs.Application.Features.Technologies.Dtos;
using Kodlama.io.Devs.Application.Features.Technologies.Commands.DeleteTechnology;
using Kodlama.io.Devs.Application.Features.Technologies.Commands.CreateTechnology;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery getListTechnologyQuery = new() { PageRequest = pageRequest };
            TechnologyListModel result = await Mediator.Send(getListTechnologyQuery);
            return Ok(result);
        }

        [HttpPost("GetList/Dynamic")]
        public async Task<ActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListTechnologyByDynamicQuery getListTechnologyByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            TechnologyListModel result = await Mediator.Send(getListTechnologyByDynamicQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateTechnologyCommand createTechnologyCommand)
        {
            
            CreatedTechnologyDto result = await Mediator.Send(createTechnologyCommand);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
        {
            UpdatedTechnologyDto result = await Mediator.Send(updateTechnologyCommand);
            return Ok(result);
        }

        [HttpPost("delete/{Id}")]
        public async Task<ActionResult> Delete([FromRoute] DeleteTechnologyCommand deleteTechnologyCommand)
        {
            DeletedTechnologyDto result = await Mediator.Send(deleteTechnologyCommand);
            return Ok(result);
        }

    }
}
