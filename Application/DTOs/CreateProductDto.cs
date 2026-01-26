using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record CreateProductDto(string Name,
        string Description,
        decimal Price,
        int Quantity);
}
