using DataAccessLayer.Models;
using Xunit;

namespace Tests
{
    public static class CategoryAssertions
    {
        public static Category ShouldBeNamed(this Category category, string expectedName)
        {
            Assert.Equal(expectedName, category.Name);
            Assert.True(category.Id > 0);
            return category;
        }

        public static Category ShouldBeATopLevelCategoryNamed(this Category category, string expectedName)
        {
            Assert.Null(category.ParentCategoryId);
            return category.ShouldBeNamed(expectedName);
        }

        public static Category ShouldBeEmpty(this Category category)
        {
            Assert.Empty(category.Products);
            Assert.Empty(category.Subcategories);
            return category;
        }

        public static Category ShouldHaveNoProducts(this Category category)
        {
            Assert.Empty(category.Products);
            return category;
        }

        public static Category ShouldHaveProducts(this Category category, params string[] productNames)
        {
            Assert.Equal(productNames.Length, category.Products.Count);

            for (int i = 0; i < productNames.Length; i++)
            {
                Assert.Equal(productNames[i], category.Products[i].Name);
                Assert.Equal(category.Id, category.Products[i].CategoryId);
            }

            return category;
        }

        public static Category ShouldHaveNoSubcategories(this Category category)
        {
            Assert.Empty(category.Subcategories);
            return category;
        }

        public static Category ShouldHaveSubcategories(this Category category, params string[] subcategoryNames)
        {
            Assert.Equal(subcategoryNames.Length, category.Subcategories.Count);

            for (int i = 0; i < subcategoryNames.Length; i++)
            {
                Assert.Equal(subcategoryNames[i], category.Subcategories[i].Name);
                Assert.Equal(category.Id, category.Subcategories[i].ParentCategoryId);
            }

            return category;
        }
    }
}
