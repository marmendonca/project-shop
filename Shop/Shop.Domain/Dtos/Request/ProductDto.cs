using Shop.Domain.Entities;

namespace Shop.Domain.Dtos.Request
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}
