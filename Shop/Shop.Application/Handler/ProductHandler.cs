using AutoMapper;
using MediatR;
using Shop.Application.Command;
using Shop.Domain.Dtos.Response;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Handler
{
    public class ProductHandler : 
        IRequestHandler<CreateProductCommand, ProductResponseDto>,
        IRequestHandler<UpdateProductCommand, string>,
        IRequestHandler<DeleteProductCommand, string>,
        IRequestHandler<GetProductByIdCommand, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductHandler(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductDto);

            _productRepository.Save(product);

            var category = _categoryRepository.GetCategoryById(product.CategoryId);

            var response = new ProductResponseDto
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                CategoryName = category.Title
            };

            return Task.FromResult(response);
        }

        public Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productFromDb = _productRepository.GetProductById(request.Id);

            if (productFromDb == null)
                throw new Exception("Id do produto não encontrado para atualização");

            var product = _mapper.Map<Product>(request.ProductDto);

            _productRepository.Update(product, request.Id);

            var response = "Produto atualizado com sucesso";

            return Task.FromResult(response);
        }

        public Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetProductById(request.Id);

            _productRepository.Delete(product.Id);

            var response = "Produto excluido com sucesso";

            return Task.FromResult(response);
        }

        public Task<ProductResponseDto> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = _productRepository.GetProductById(request.Id);
            var category = _categoryRepository.GetCategoryById(product.CategoryId);

            var response = new ProductResponseDto
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                CategoryName = category.Title
            };

            return Task.FromResult(response);
        }
    }
}
