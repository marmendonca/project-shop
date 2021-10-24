using Shop.Domain.Entities;
using System.Collections.Generic;

namespace Shop.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        void Save(Product product);

        void Update(Product product, int id);

        Product GetProductById(int id);

        List<Product> GetAll();

        void Delete(int id);
    }
}
