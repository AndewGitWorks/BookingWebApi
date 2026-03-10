using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Product
{
    public record CreateProductDto(string Name,
        string Description,
        decimal Price,
        int Quantity);
}