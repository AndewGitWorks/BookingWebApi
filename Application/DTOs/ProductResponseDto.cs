using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record ProductResponseDto(string Title, string Description, decimal Price, int Quantity);
    
}
