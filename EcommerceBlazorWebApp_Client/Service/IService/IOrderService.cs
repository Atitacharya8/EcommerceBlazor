using Ecommerce_Models;

namespace EcommerceBlazorWebApp_Client.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId);
        public Task<OrderDTO> Get(int orderId);


    }
}
