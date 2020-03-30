using System.Threading.Tasks;
using Application.Games.Commands.CreateGame;
using Application.Games.Commands.DeleteGame;
using Application.Games.Commands.UpdateGame;
using Application.Games.Queries.GetGameDetail;
using Application.Games.Queries.GetGamesList;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class GamesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<GamesListVm>> GetAll([FromQuery] GetGamesListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDetailDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetGameDetailQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<GameDetailDto>> Create([FromBody] CreateGameCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetGameDetailQuery { Id = id }));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<GameDetailDto>> Update(int id, [FromBody] UpdateGameCommand command)
        {
            command.Id = id;
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetGameDetailQuery { Id = res }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteGameCommand { Id = id });
            return NoContent();
        }
    }
}