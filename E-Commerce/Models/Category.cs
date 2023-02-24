using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]

        public string Description { get; set; }
    }
}
