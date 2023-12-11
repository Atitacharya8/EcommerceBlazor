using EcommerceBlazorWebApp_Client.ViewModels;

namespace EcommerceBlazorWebApp_Client.Service.IService
{
    public interface ICartService
    {
        Task DecrementCart(ShoppingCart shoppingCart);
        Task IncrementCart(ShoppingCart shoppingCart);
    }
}
