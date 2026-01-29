using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController
    {
        // Implementation will go here
        private readonly IProductInterface _product;
        public ProductController(IProductInterface product)
        {
            _product = product;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateProduct(CreateProductDto request)
        {
            await _product.CreateProductAsync(request);
            return new OkResult();
        }
        [HttpGet]
        [Route("getAll")]
        public async Task<ICollection<GetProductByName>> GetProductsByName([FromQuery]string name)
        {
            return await _product.GetProductsByNameAsync(name);
        }
    }
}
