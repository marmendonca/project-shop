using AutoMapper;
using MediatR;
using Shop.Application.Command;
using Shop.Domain.Dtos.Response;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Handler
{
    public class CategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponseDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Task<CategoryResponseDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.CategoryDto);

            _categoryRepository.Save(category);

            var response = new CategoryResponseDto();

            return Task.FromResult(response);
        }
    }
}
