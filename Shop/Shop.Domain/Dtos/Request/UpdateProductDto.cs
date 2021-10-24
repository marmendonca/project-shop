namespace Shop.Domain.Dtos.Request
{
    public class UpdateProductDto
    {
        public UpdateProductDto(ProductDto productDto, int id)
        {
            ProductDto = productDto;
            Id = id;
        }

        public ProductDto ProductDto { get; set; }

        public int Id { get; set; }
    }
}
