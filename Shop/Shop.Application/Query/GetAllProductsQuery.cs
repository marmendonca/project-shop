using MediatR;
using Shop.Domain.Entities;
using System.Collections.Generic;

namespace Shop.Application.Query
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }
}
