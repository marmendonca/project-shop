﻿namespace Shop.Domain.Dtos.Response
{
    public class ProductResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }
    }
}
