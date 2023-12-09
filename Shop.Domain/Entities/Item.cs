namespace Shop.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public decimal Price { get; set; } 
        public string Description { get; set; } 
        public int StockQuantity { get; set; }
        public string ProductImage { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string EncodedName { get; set; } = default!;
        public void EncodeName() => EncodedName = "item"+ Name.ToLower().Replace(" ", "");
    }
}
