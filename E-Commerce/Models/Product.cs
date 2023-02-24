namespace E_Commerce.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public Category Category { get; set; }
    }
}

