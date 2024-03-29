﻿namespace Shop.Application.Category
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Domain.Entities.Item> Items { get; set; } = new List<Domain.Entities.Item>();
        public string EncodedName { get; set; }
    }
}
