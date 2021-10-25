using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Command;
using Shop.Application.Query;
using System.Threading.Tasks;

namespace Shop.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCategorysQuery();
            var categorys = await _mediator.Send(query);

            return Ok(categorys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdCommand(id));

            return Ok(result);
        }
    }
}
