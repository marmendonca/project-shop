using AutoMapper;
using MediatR;
using Shop.Application.Command;
using Shop.Application.Query;
using Shop.Domain.Dtos.Response;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Handler
{
    public class ProductHandler : 
        IRequestHandler<CreateProductCommand, ProductResponseDto>,
        IRequestHandler<UpdateProductCommand, ProductResponseDto>,
        IRequestHandler<DeleteProductCommand, string>,
        IRequestHandler<GetProductByIdCommand, ProductResponseDto>,
        IRequestHandler<GetAllProductsQuery, List<Product>>
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

        public Task<ProductResponseDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productFromDb = _productRepository.GetProductById(request.Id);

            if (productFromDb == null)
                throw new Exception("Id do produto não encontrado para atualização");

            var product = _mapper.Map<Product>(request.ProductDto);

            _productRepository.Update(product, request.Id);

            var response = new ProductResponseDto() 
            {
                Id = request.Id,
                Title = product.Title,
                Price = product.Price
            };

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

        public Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _productRepository.GetAll();

            return Task.FromResult(products);
        }
    }
}
