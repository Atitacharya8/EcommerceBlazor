using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter name")]
        public string Name { get; set; }
    }
}
