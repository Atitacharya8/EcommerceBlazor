using Ecommerce_Models;

namespace Ecommerce_Business.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> Create (OrderDTO objDTO);
        public Task<OrderDTO> Get(int id);
        //get all the order with certain userid or with certain status
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null);
        public Task<int> Delete(int id);
        public Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO);
        public Task<OrderHeaderDTO> MarkPaymentSuccessful(int id);
        public Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
