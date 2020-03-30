using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable 1998

namespace WebUI.Controllers
{
    public class OrdersController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // TODO: implement
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // TODO: implement
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            // TODO: implement
            return Ok();
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            // TODO: implement
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: implement
            return NoContent();
        }
    }
}