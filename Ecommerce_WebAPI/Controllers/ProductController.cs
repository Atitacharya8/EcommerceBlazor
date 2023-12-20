using Ecommerce_Business.Repository.IRepository;
using Ecommerce_Common;
using Ecommerce_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Customer)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAll()); //its api so we pass it using ok keyword
        }


        [HttpGet("{productId}")]
        // [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int? productId)
        {
            if(productId == null || productId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    
                    ErrorMessage="Invalid Id",                 
                    StatusCode= StatusCodes.Status400BadRequest

                });
            }

            var product = await _productRepository.Get(productId.Value);

            if(product == null) // here we have id as nullable so we have to check if product is null nor has value
            {
                return BadRequest(new ErrorModelDTO() {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
                   
            }

            return Ok(product);
        }
    }
}
 