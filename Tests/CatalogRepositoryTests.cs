using System.Collections.Generic;
using DataAccessLayer.Models;
using Xunit;

namespace Tests
{
    public class CatalogRepositoryTests : CatalogRepositoryTestBase
    {
        [Fact]
        public void Create_Empty_Catalog()
        {
            var catalog = new Catalog
            {
                Name = "Department Store"
            };

            WhenCatalogIsCreated(catalog);
            var resultCatalog = ThenThereShouldBeOneResult();

            resultCatalog
                .ShouldBeNamed("Department Store")
                .ShouldBeEmpty();
        }

        [Fact]
        public void Create_Catalog_With_Empty_Categories()
        {
            var catalog = new Catalog
            {
                Name = "Department Store",
                Categories = new List<Category>
                {
                    new Category { Name = "Toys" },
                    new Category { Name = "Home & Garden" }
                }
            };

            WhenCatalogIsCreated(catalog);
            var resultCatalog = ThenThereShouldBeOneResult();

            resultCatalog
                .ShouldBeNamed("Department Store")
                .ShouldHaveCategories("Toys", "Home & Garden");

            resultCatalog.Categories[0]
                .ShouldBeATopLevelCategoryNamed("Toys")
                .ShouldBeEmpty();

            resultCatalog.Categories[1]
                .ShouldBeATopLevelCategoryNamed("Home & Garden")
                .ShouldBeEmpty();
        }

        [Fact]
        public void Create_Catalog_With_Categories_And_Products()
        {
            var catalog = new CatalogBuilder()
                .AddCategory("Toys")
                    .AddProduct("Rubik's Cube")
                    .AddProduct("Etch A Sketch")
                .AddCategory("Home & Garden")
                    .AddProduct("Adirondack Chair")
                .Build();

            WhenCatalogIsCreated(catalog);
            var resultCatalog = ThenThereShouldBeOneResult();

            resultCatalog
                .ShouldBeNamed("Department Store")
                .ShouldHaveCategories("Toys", "Home & Garden");

            resultCatalog.Categories[0]
                .ShouldBeATopLevelCategoryNamed("Toys")
                .ShouldHaveProducts("Rubik's Cube", "Etch A Sketch")
                .ShouldHaveNoSubcategories();

            resultCatalog.Categories[1]
                .ShouldBeATopLevelCategoryNamed("Home & Garden")
                .ShouldHaveProducts("Adirondack Chair")
                .ShouldHaveNoSubcategories();
        }

        [Fact]
        public void Create_Catalog_With_Categories_And_Subcategories_And_Products()
        {
            var catalog = new CatalogBuilder()
                .AddCategory("Toys")
                    .AddSubcategory("Puzzles")
                        .AddProduct("Rubik's Cube")
                    .AddSubcategory("Games")
                        .AddProduct("Battleship")
                        .AddProduct("Monopoly")
                .Build();

            WhenCatalogIsCreated(catalog);
            var resultCatalog = ThenThereShouldBeOneResult();

            resultCatalog
                .ShouldBeNamed("Department Store")
                .ShouldHaveCategories("Toys", "Puzzles", "Games");

            resultCatalog.Categories[0]
                .ShouldBeATopLevelCategoryNamed("Toys")
                .ShouldHaveNoProducts()
                .ShouldHaveSubcategories("Puzzles", "Games");

            resultCatalog.Categories[0].Subcategories[0]
                .ShouldBeNamed("Puzzles")
                .ShouldHaveProducts("Rubik's Cube")
                .ShouldHaveNoSubcategories();

            resultCatalog.Categories[0].Subcategories[1]
                .ShouldBeNamed("Games")
                .ShouldHaveProducts("Battleship", "Monopoly")
                .ShouldHaveNoSubcategories();
        }

        [Fact]
        public void When_Creating_Multiple_Catalogs_Populated_With_Same_Ids_Then_Catalogs_Are_Saved_With_New_Ids()
        {
            var catalog1 = new Catalog
            {
                Id = 1, Name = "Department Store1",
                Categories = new List<Category>
                {
                    new Category
                    {
                        Id = 1, CatalogId = 1, Name = "Toys",
                        Subcategories = new List<Category>
                        {
                            new Category { Id = 2, CatalogId = 1, ParentCategoryId = 1, Name = "Puzzles" },
                            new Category { Id = 3, CatalogId = 1, ParentCategoryId = 1, Name = "Games" }
                        }
                    }
                }
            };

            var catalog2 = new Catalog
            {
                Id = 1,
                Name = "Department Store2",
                Categories = new List<Category>
                {
                    new Category
                    {
                        Id = 1, CatalogId = 1, Name = "Toys",
                        Subcategories = new List<Category>
                        {
                            new Category { Id = 2, CatalogId = 1, ParentCategoryId = 1, Name = "Puzzles" },
                            new Category { Id = 3, CatalogId = 1, ParentCategoryId = 1, Name = "Games" }
                        }
                    }
                }
            };

            GivenIdentitySeedsAreReset();
            WhenCatalogsAreCreated(catalog1, catalog2);
            var resultCatalogs = ThenThereShouldBeResults(2);

            resultCatalogs[0]
                .ShouldBeNamed("Department Store1")
                .ShouldHaveId(1)
                .ShouldHaveCategories("Toys", "Puzzles", "Games");

            resultCatalogs[1]
                .ShouldBeNamed("Department Store2")
                .ShouldHaveId(2)
                .ShouldHaveCategories("Toys", "Puzzles", "Games");
        }
    }
}
