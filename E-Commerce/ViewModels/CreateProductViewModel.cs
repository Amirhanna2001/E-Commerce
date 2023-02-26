using E_Commerce.Models;

namespace E_Commerce.ViewModels
{
    public class CreateProductViewModel
    {
        public int ProductId { get; set; }//
        public int Quantity { get; set; }//
        public string Name { get; set; }//
        public string Brand { get; set; }//
        public int CategoryId { get; set; }//
        public string Description { get; set; }//
        public double Price { get; set; }
        public byte[] ?Image { get; set; }
        public List<Category>? Categories { get;  set; }
    }
}
