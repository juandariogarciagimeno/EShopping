﻿namespace EShopping.Catalog.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<string> Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageFile { get; set; } = null!;
    }
}
