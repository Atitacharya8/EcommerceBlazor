using Ecommerce_Models;
using EcommerceBlazorWebApp_Client.Service.IService;

namespace EcommerceBlazorWebApp_Client.Service
{
    public class ProductService : IProductService
    {
        public Task<ProductDTO> Get(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
