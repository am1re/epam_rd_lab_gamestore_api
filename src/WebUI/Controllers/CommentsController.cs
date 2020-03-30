using System.Threading.Tasks;
using Application.Сomments.Commands.CreateComment;
using Application.Сomments.Commands.DeleteComment;
using Application.Сomments.Commands.UpdateComment;
using Application.Сomments.Queries.GetCommentDetail;
using Application.Сomments.Queries.GetCommentsList;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CommentsController : BaseController
    {
        [HttpGet("/Games/{id}/Comments/")]
        public async Task<ActionResult<CommentsListVm>> GetAllByGameId(int id, [FromQuery] GetCommentsListQuery query)
        {
            query.GameId = id;
            return Ok(await Mediator.Send(query));
        }
        
        [HttpGet]
        public async Task<ActionResult<CommentsListVm>> GetAll([FromQuery] GetCommentsListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDetailVm>> Get(int id, [FromQuery] GetCommentDetailQuery query)
        {
            query.Id = id;
            return Ok(await Mediator.Send(query));
        }
        
        [HttpPost]
        public async Task<ActionResult<CommentDetailVm>> Create([FromBody] CreateCommentCommand command)
        {
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetCommentDetailQuery{Id = res}));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentDetailVm>> Update(int id, [FromBody] UpdateCommentCommand command)
        {
            command.Id = id;
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetCommentDetailQuery{Id = res}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCommentCommand { Id = id });
            return NoContent();
        }
    }
}