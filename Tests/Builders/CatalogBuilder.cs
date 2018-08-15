using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;

namespace Tests
{
    public class CatalogBuilder
    {
        private Catalog _catalog;
        private Category _currentCategory;

        public CatalogBuilder()
        {
            _catalog = new Catalog
            {
                Name = "Department Store",
                Categories = new List<Category>()
            };
        }

        public Catalog Build()
        {
            return _catalog;
        }

        public CatalogBuilder AddCategory(string name)
        {
            _currentCategory = BuildCategory(name);
            _catalog.Categories.Add(_currentCategory);
            return this;
        }

        public CatalogBuilder AddSubcategory(string name)
        {
            var subcategory = BuildCategory(name);

            // This will always add subcategory to the last top-level category
            // so it limits the builder to one level of nesting
            _catalog.Categories.Last().Subcategories.Add(subcategory);
            _currentCategory = subcategory;
            return this;
        }

        public CatalogBuilder AddProduct(string name)
        {
            var product = new Product
            {
                Name = name
            };

            _currentCategory.Products.Add(product);
            return this;
        }

        private Category BuildCategory(string name)
        {
            return new Category
            {
                Name = name,
                Subcategories = new List<Category>(),
                Products = new List<Product>()
            };
        }
    }
}
