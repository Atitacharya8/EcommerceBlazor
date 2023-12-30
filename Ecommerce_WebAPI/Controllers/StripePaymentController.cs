using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_WebAPI.Controllers
{
    public class StripePaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
