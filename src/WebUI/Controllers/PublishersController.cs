using System.Threading.Tasks;
using Application.Publishers.Commands.CreatePublisher;
using Application.Publishers.Commands.DeletePublisher;
using Application.Publishers.Commands.UpdatePublisher;
using Application.Publishers.Queries;
using Application.Publishers.Queries.GetPublisherDetail;
using Application.Publishers.Queries.GetPublishersList;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PublishersController : BaseController
    {
        [HttpGet("/Games/{id}/Publishers/")]
        public async Task<ActionResult<PublishersListVm>> GetAllByGameId(int id, [FromQuery] GetPublishersListQuery query)
        {
            query.GameId = id;
            return Ok(await Mediator.Send(query));
        }
        
        [HttpGet]
        public async Task<ActionResult<PublishersListVm>> GetAll([FromQuery] GetPublishersListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPublisherDetailQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDto>> Create([FromBody] CreatePublisherCommand command)
        {
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetPublisherDetailQuery{Id = res}));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<PublisherDto>> Update(int id, [FromBody] UpdatePublisherCommand command)
        {
            command.Id = id;
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetPublisherDetailQuery{Id = res}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePublisherCommand { Id = id });
            return NoContent();
        }
    }
}