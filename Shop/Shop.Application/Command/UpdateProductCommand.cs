using MediatR;
using Shop.Domain.Dtos.Request;
using Shop.Domain.Dtos.Response;

namespace Shop.Application.Command
{
    public class UpdateProductCommand : IRequest<ProductResponseDto>
    {
        public UpdateProductCommand(ProductDto productDto, int id)
        {
            ProductDto = productDto;
            Id = id;
        }

        public int Id { get; set; }

        public ProductDto ProductDto { get; set; }
    }
}
