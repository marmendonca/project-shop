using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Command;
using Shop.Application.Query;
using Shop.Domain.Dtos.Request;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdCommand(id), CancellationToken.None);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Requisição invalida");

            var result = await _mediator.Send(new CreateProductCommand(productDto));

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("Requisição invalida");

            var result = await _mediator.Send(new UpdateProductCommand(productDto, id));

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id), CancellationToken.None);

            return Ok();
        }
    }
}
