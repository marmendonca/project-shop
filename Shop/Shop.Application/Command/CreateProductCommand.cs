using MediatR;
using Shop.Domain.Dtos.Request;
using Shop.Domain.Dtos.Response;

namespace Shop.Application.Command
{
    public class CreateProductCommand : IRequest<ProductResponseDto>
    {
        public CreateProductCommand(ProductDto productDto)
        {
            ProductDto = productDto;
        }

        public ProductDto ProductDto { get; set; }
    }
}
