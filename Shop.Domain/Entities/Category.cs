namespace Shop.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public string EncodedName { get; set; } = default!;
        public void EncodeName() => EncodedName = "category" + Name.ToLower().Replace(" ", "");
    }
}
