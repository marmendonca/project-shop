using AutoMapper;
using MediatR;
using Shop.Application.Command;
using Shop.Domain.Dtos.Request;
using Shop.Domain.Dtos.Response;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.Handler
{
    public class ProductHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICat
        private readonly IMapper _mapper;

        public ProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductDto);

            _productRepository.Save(product);

            var response = new ProductResponseDto();
            return Task.FromResult(response);
        }
    }
}
