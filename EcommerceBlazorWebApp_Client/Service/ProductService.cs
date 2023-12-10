using Ecommerce_DataAccess;
using Ecommerce_Models;
using EcommerceBlazorWebApp_Client.Service.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EcommerceBlazorWebApp_Client.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string BaseServerUrl;
        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {

            _httpClient = httpClient;
             _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
    }
        public async Task<ProductDTO> Get(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{productId}"); //string interpolation to add productID inside url
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                product.ImageUrl = BaseServerUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);

            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                foreach(var prod in products)
                {
                    prod.ImageUrl = BaseServerUrl + prod.ImageUrl;
                }
                return products;
            }

            //if we don't get anything a empty list
            return new List<ProductDTO>();
        }
    }
}
