using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using Xunit;

namespace Tests
{
    public static class CatalogAssertions
    {
        public static Catalog ShouldBeNamed(this Catalog catalog, string expectedName)
        {
            Assert.Equal(expectedName, catalog.Name);
            Assert.True(catalog.Id > 0);
            return catalog;
        }

        public static Catalog ShouldHaveId(this Catalog catalog, int expectedId)
        {
            Assert.Equal(expectedId, catalog.Id);
            return catalog;
        }

        public static Catalog ShouldBeEmpty(this Catalog catalog)
        {
            Assert.Empty(catalog.Categories);
            return catalog;
        }

        public static Catalog ShouldHaveCategories(this Catalog catalog, params string[] categoryNames)
        {
            AssertCountsAreEqual(categoryNames, catalog.Categories.Select(c => c.Name));

            for (int i = 0; i < categoryNames.Length; i++)
            {
                Assert.Equal(categoryNames[i], catalog.Categories[i].Name);
                Assert.Equal(catalog.Id, catalog.Categories[i].CatalogId);
            }

            return catalog;
        }

        private static void AssertCountsAreEqual(IEnumerable<string> expected, IEnumerable<string> actual)
        {
            try
            {
                Assert.Equal(expected.Count(), actual.Count());
            }
            catch (Exception ex)
            {
                var message = $"Assert.Equal() Failure{Environment.NewLine}Expected: {string.Join(", ", expected)}{Environment.NewLine}Actual: {string.Join(", ", actual)}";
                throw new Exception(message, ex);
            }
        }
    }
}
