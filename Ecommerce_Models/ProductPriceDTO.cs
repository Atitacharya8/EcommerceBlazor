using Ecommerce_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Models
{
    public class ProductPriceDTO
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [Required]
        public Product Product { get; set; }
        [Required]
        public string Size { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Price must be greater than 1")]
        public double Price { get; set; }

        public ICollection<ProductPriceDTO> ProductPrices { get; set; }

    }
}
