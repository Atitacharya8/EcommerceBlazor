using Ecommerce_Models;
using EcommerceBlazorWebApp_Client.ViewModels;
using System.ComponentModel.Design;

namespace EcommerceBlazorWebApp_Client.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessModelDTO> Checkout(StripePaymentDTO model);
    }
       
}
