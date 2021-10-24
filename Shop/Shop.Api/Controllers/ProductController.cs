using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Command;
using Shop.Domain.Dtos.Request;
using Shop.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IProductRepository _productRepository;

        public ProductController(IMediator mediator, IProductRepository productRepository)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productRepository.GetAll();
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
            var result = await _mediator.Send(new DeleteProductCommand(id), CancellationToken.None);

            return Ok(result);
        }
    }
}
