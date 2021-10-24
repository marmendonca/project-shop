using MediatR;
using Shop.Domain.Dtos.Request;

namespace Shop.Application.Command
{
    public class UpdateProductCommand : IRequest<string>
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
