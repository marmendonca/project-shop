namespace Shop.Domain.Dtos.Request
{
    public class ProductDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
