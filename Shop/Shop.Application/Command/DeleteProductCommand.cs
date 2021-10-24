using MediatR;

namespace Shop.Application.Command
{
    public class DeleteProductCommand : IRequest<string>
    {
        public DeleteProductCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

