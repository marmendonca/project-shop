using AutoMapper;
using MediatR;
using Shop.Application.Command;
using Shop.Application.Query;
using Shop.Domain.Dtos.Response;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Handler
{
    public class CategoryHandler : 
        IRequestHandler<CreateCategoryCommand, CategoryResponseDto>,
        IRequestHandler<GetCategoryByIdCommand, CategoryResponseDto>,
        IRequestHandler<GetAllCategorysQuery, List<Category>>
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

        public Task<CategoryResponseDto> Handle(GetCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.GetCategoryById(request.CategoryId);

            var response = new CategoryResponseDto
            {
                Id = category.Id,
                Title = category.Title
            };

            return Task.FromResult(response);
        }

        public Task<List<Category>> Handle(GetAllCategorysQuery request, CancellationToken cancellationToken)
        {
            var categorys = _categoryRepository.GetAll();

            return Task.FromResult(categorys);
        }
    }
}
