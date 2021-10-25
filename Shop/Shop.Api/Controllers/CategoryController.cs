using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Command;
using Shop.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IMediator mediator, ICategoryRepository categoryRepository)
        {
            _mediator = mediator;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categorys = _categoryRepository.GetAll();
            return Ok(categorys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdCommand(id));

            return Ok(result);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
