using API.Models;
using Application.DTOs.Product;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface _product;
        private readonly IOrderInterface _order;
        public ProductController(IProductInterface product,
            IOrderInterface order)
        {
            _product = product;
            _order = order;
        }
        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct(CreateProductDto request, CancellationToken cancellationToken)
        {
            await _product.CreateProductAsync(request, cancellationToken);
            return Ok("Product has been created");
        }
        // [HttpGet]
        // [Route("getAllbyName")]
        // public async Task<ICollection<ProductResponseDto>> GetProductsByName([FromQuery] string name)
        // {
        //     return await _product.GetProductsByNameAsync(name);
        // }
        [HttpGet]
        [Route("catalog")]
        public async Task<PagedResponse<ProductListResponse>> GetAllProducts([FromQuery]ProductSortModel sort, CancellationToken cancellationToken)
        {
            var response = await _product.GetAllAsync(cancellationToken: cancellationToken);
            if(sort.IsAscending && sort.IsDescending)
            {
                throw new ArgumentException("Cannot sort by both ascending and descending order.");
            }
            if (sort.IsDescending)
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
        [Route("{productId}/update")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid productId, [FromBody] UpdateProductDto request, CancellationToken cancellationToken)
        {
            try
            {
                await _product.UpdateProductAsync(request, productId, cancellationToken);
                return new OkResult();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        [Route("{productId}/add/{orderId}")]
        public async Task<IActionResult> AddProductToOrder([FromQuery] string token, [FromRoute] Guid orderId, [FromRoute] Guid productId, CancellationToken cancellationToken)
        {
            await _order.AddProductAsync(token, orderId, productId, cancellationToken);
            return Ok("Product has been added");
        }
        [HttpDelete]
        [Route("{productId}/delete")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId, CancellationToken cancellationToken)
        {
            try
            {
                await _product.DeleteProductAsync(productId, cancellationToken);
                return Ok("Product has been deleted");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("{productId}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductAsync([FromRoute]Guid productId, CancellationToken cancellationToken)
        {
            var response = await _product.GetProductForResponseAsync(productId, cancellationToken);
            return Ok(response);
        }
    }
}
