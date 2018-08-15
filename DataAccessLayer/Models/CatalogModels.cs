using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Catalog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogId { get; set; }

        [ForeignKey(nameof(Subcategories))]
        public int? ParentCategoryId { get; set; }

        public List<Category> Subcategories { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
