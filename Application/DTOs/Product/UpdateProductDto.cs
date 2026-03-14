public record UpdateProductDto(Guid Id,string? Name,
    string? Description,
    decimal? Price,
    int? Quantity);