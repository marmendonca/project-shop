﻿using MediatR;
using Shop.Domain.Dtos.Response;

namespace Shop.Application.Command
{
    public class GetCategoryByIdCommand : IRequest<CategoryResponseDto>
    {
        public int CategoryId { get; set; }
    }
}
