using Ecommerce_Business.Repository.IRepository;
using Ecommerce_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll()); //its api so we pass it using ok keyword
        }


        [HttpGet("{orderHeaderId}")]
        // [ProducesResponseType(400)]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            if(orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    
                    ErrorMessage="Invalid Id",                 
                    StatusCode= StatusCodes.Status400BadRequest

                });
            }

            var orderHeader = await _orderRepository.Get(orderHeaderId.Value);

            if(orderHeader == null) // here we have id as nullable so we have to check if product is null nor has value
            {
                return BadRequest(new ErrorModelDTO() {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
                   
            }

            return Ok(orderHeader);
        }

        [HttpPost]
        [ActionName("create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            paymentDTO.Order.OrderHeader.OrderDate = DateTime.Now;
            var result = await _orderRepository.Create(paymentDTO.Order);
            return Ok(result);
        }
    }
}
 