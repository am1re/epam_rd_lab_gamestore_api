using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Developers.Queries;
using Application.Developers.Commands.CreateDeveloper;
using Application.Developers.Commands.DeleteDeveloper;
using Application.Developers.Commands.UpdateDeveloper;
using Application.Developers.Queries.GetDeveloperDetail;
using Application.Developers.Queries.GetDevelopersList;

namespace WebUI.Controllers
{
    public class DevelopersController : BaseController
    {
        [HttpGet("/Games/{id}/Developers/")]
        public async Task<ActionResult<DevelopersListVm>> GetAllByGameId(int id, [FromQuery] GetDevelopersListQuery query)
        {
            query.GameId = id;
            return Ok(await Mediator.Send(query));
        }
        
        [HttpGet]
        public async Task<ActionResult<DevelopersListVm>> GetAll([FromQuery] GetDevelopersListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetDeveloperDetailQuery { Id = id }));
        }
        
        [HttpPost]
        public async Task<ActionResult<DeveloperDto>> Create([FromBody] CreateDeveloperCommand command)
        {
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetDeveloperDetailQuery{Id = res}));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<DeveloperDto>> Update(int id, [FromBody] UpdateDeveloperCommand command)
        {
            command.Id = id;
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetDeveloperDetailQuery{Id = res}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteDeveloperCommand { Id = id });
            return NoContent();
        }
    }
}