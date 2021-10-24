using MediatR;
using Shop.Domain.Dtos.Request;
using Shop.Domain.Dtos.Response;

namespace Shop.Application.Command
{
    public class CreateCategoryCommand : IRequest<CategoryResponseDto>
    {
        public CreateCategoryCommand(CategoryDto categoryDto)
        {
            CategoryDto = categoryDto;
        }

        public CategoryDto CategoryDto { get; set; }
    }
}
