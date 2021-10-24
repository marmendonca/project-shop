using MediatR;
using Shop.Domain.Dtos.Response;

namespace Shop.Application.Command
{
    public class GetProductByIdCommand : IRequest<ProductResponseDto>
    {
        public GetProductByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
