//using Application.DTOs;
//using Application.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class ProductsController
//    {
//        private readonly IProductInterface _product;
//        public ProductsController(IProductInterface productInterface)
//        {
//            _product = productInterface;
//        }
//        [HttpPost]
//        [Authorize]
//        [Route("create")]
//        public async Task<IActionResult> Create([FromBody] CreateProductDto request)
//        {
//            var result = await _product.CreateProduct(request);
//            return result ? new OkResult() : new BadRequestResult();
//        }
//        [HttpGet]
//        [Route("{name}")]
//        public async Task<ActionResult<ProductResponseDto>> GetProduct([FromRoute]string name)
//        {
//            if(string.IsNullOrEmpty(name))
//            {
//                throw new Exception("Searchstring for product cannot be empty");
//            }
//            return await _product.GetProduct(name);
//        }
//    }
//}
