namespace Shop.Application.Item
{
    public class ItemDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public string ProductImage { get; set; }
        public Domain.Entities.Category Category { get; set; }
        public int CategoryId { get; set; }
        public string EncodedName { get; set; }
    }
}
