using API.Models;
using Application.DTOs.Product;
using Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<ICollection<ProductResponseDto>> GetProductsByName([FromQuery] string name)
        {
            return await _product.GetProductsByNameAsync(name);
        }
        [HttpGet]
        [Route("getall")]
        public async Task<ICollection<ProductListResponse>> GetAllProducts([FromQuery]ProductSortModel sort)
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
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid id)
        {
            try
            {
                await _product.DeleteProductAsync(id);
                return new OkResult();
            }
            catch (Exception)
            {
                return new NotFoundResult();
            }
        }
        [HttpGet]
        [Route("/products")]
        public async Task<ActionResult<ProductResponseDto>> GetProductAsync([FromQuery]Guid productId)
        {
            var response = await _product.GetProductForResponseAsync(productId);
            return response;
        }
    }
}
