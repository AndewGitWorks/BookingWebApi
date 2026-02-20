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
        private readonly IOrderInterface _order;
        public ProductController(IProductInterface product,
            IOrderInterface order)
        {
            _product = product;
            _order = order;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateProduct(CreateProductDto request)
        {
            await _product.CreateProductAsync(request);
            return new OkResult();
        }
        [HttpGet]
        [Route("getAllbyName")]
        public async Task<ICollection<GetProductByName>> GetProductsByName([FromQuery] string name)
        {
            return await _product.GetProductsByNameAsync(name);
        }
        [HttpGet]
        [Route("getall")]
        public async Task<ICollection<ProductListResponse>> GetAllProducts()
        {
            var response = await _product.GetAllAsync();
            return response;
        }
        [HttpPost]
        [Route("addProductToOrder")]
        public async Task<IActionResult> AddProductToOrder([FromQuery] string token, [FromQuery] Guid orderId, [FromQuery] Guid productId)
        {
            await _order.AddProductAsync(token, orderId, productId);
            return new OkResult();
        }
    }
}
