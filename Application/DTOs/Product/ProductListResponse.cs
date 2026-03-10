using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Product
{
    public record ProductListResponse(string Name, string Description, decimal Price, int Quantity);
    
}
