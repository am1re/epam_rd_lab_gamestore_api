using System.Threading.Tasks;
using Application.Categories.Commands.CreateCategory;
using Application.Categories.Commands.DeleteCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries;
using Application.Categories.Queries.GetCategoriesList;
using Application.Categories.Queries.GetCategoryDetail;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpGet("/Games/{id}/Categories")]
        public async Task<ActionResult<CategoriesListVm>> GetByGameId(int id, [FromQuery] GetCategoriesListQuery query)
        {
            query.GameId = id;
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        public async Task<ActionResult<CategoriesListVm>> GetAll([FromQuery] GetCategoriesListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryDetailQuery {Id = id}));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryCommand command)
        {
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetCategoryDetailQuery {Id = res}));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = id;
            var res = await Mediator.Send(command);
            return Ok(await Mediator.Send(new GetCategoryDetailQuery {Id = res}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand {Id = id});
            return NoContent();
        }
    }
}