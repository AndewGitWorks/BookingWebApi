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
        // [HttpGet]
        // [Route("getAllbyName")]
        // public async Task<ICollection<ProductResponseDto>> GetProductsByName([FromQuery] string name)
        // {
        //     return await _product.GetProductsByNameAsync(name);
        // }
        [HttpGet]
        [Route("/show")]
        public async Task<PagedResponse<ProductListResponse>> GetAllProducts([FromQuery]ProductSortModel sort)
        {
            var response = await _product.GetAllAsync();
            if(sort.IsAscending && sort.IsDescending)
            {
                throw new ArgumentException("Cannot sort by both ascending and descending order.");
            }
            if(sort.IsDescending)
            {
                response = response.OrderByDescending(p => p.Price).ToList();
            }
            else if(sort.IsAscending)
            {
                response = response.OrderBy(p => p.Price).ToList();
            }
            
            if(sort.MinPrice.HasValue)
            {
                response = response.Where(p => p.Price >= sort.MinPrice.Value).ToList();
            }
            if(sort.MaxPrice.HasValue)
            {
                response = response.Where(p => p.Price <= sort.MaxPrice.Value).ToList();
            }
            if(!string.IsNullOrWhiteSpace(sort.Search))
            {
                // response = response.Where(p => p.Name.Contains(sort.Search)).ToList();
                response = (List<ProductListResponse>)response.Where(p => 
            p.Name.Contains(sort.Search, StringComparison.OrdinalIgnoreCase));
            }
            var page = sort.Page ?? 1;
            var pageSize = sort.PageSize ?? 10;
    
            var items = response
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            var finalResponse = new PagedResponse<ProductListResponse>
            {
            Items = items,
            TotalCount = response.Count(),
            Page = page,
            PageSize = pageSize
            };
            return finalResponse;
        }
        [HttpPatch]
        [Route("/{id}/update")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDto request)
        {
            try
            {
                await _product.UpdateProductAsync(request, id);
                return new OkResult();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        [Route("addToOrder")]
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
