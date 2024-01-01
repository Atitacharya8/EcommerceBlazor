using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Models
{
    public class StripePaymentDTO
    {
        public StripePaymentDTO()
        {
            SuccessUrl = "OrderConfirmation";
            CancelUrl = "Summary";
        }
       // [ValidateNever] --> using this line of code is not compatible by web assembly because it is not in the client side project
        public OrderDTO Order { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }

    }
}
