using Shop.Domain.Entities;
using System.Collections.Generic;

namespace Shop.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        void Save(Category category);

        void Update(Category category);

        Category GetCategoryById(int id);

        List<Category> GetAll();

        void Delete(int id);
    }
}
