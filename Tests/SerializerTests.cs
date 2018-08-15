using BusinessLogic;
using DataAccessLayer;
using DataAccessLayer.Models;
using Xunit;

namespace Tests
{
    public class SerializeTests : CatalogRepositoryTestBase
    {
        private string _result;

        private void GivenTheCatalogHasBeenCreated(Catalog catalog)
        {
            GivenIdentitySeedsAreReset();
            var repository = new CatalogRepository();
            repository.Create(catalog);
        }

        private void WhenTheCatalogIsSerialized(int catalogId)
        {
            var serializer = new Serializer();
            _result = serializer.Serialize(catalogId);
        }

        private void ThenTheResultIs(string expectedResult)
        {
            Assert.Equal(expectedResult, _result);
        }

        [Fact]
        public void Serialize_Catalog_With_Categories_And_Products()
        {
            var catalog = new CatalogBuilder()
                .AddCategory("Toys")
                    .AddProduct("Rubik's Cube")
                    .AddProduct("Etch A Sketch")
                .AddCategory("Home & Garden")
                    .AddProduct("Adirondack Chair")
                .Build();

            GivenTheCatalogHasBeenCreated(catalog);
            WhenTheCatalogIsSerialized(catalog.Id);
            ThenTheResultIs(
@"<?xml version=""1.0"" encoding=""utf-16""?>
<Catalog xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Id>1</Id>
  <Name>Department Store</Name>
  <Categories>
    <Category>
      <Id>1</Id>
      <Name>Toys</Name>
      <CatalogId>1</CatalogId>
      <ParentCategoryId xsi:nil=""true"" />
      <Subcategories />
      <Products>
        <Product>
          <Id>1</Id>
          <Name>Rubik's Cube</Name>
          <CategoryId>1</CategoryId>
        </Product>
        <Product>
          <Id>2</Id>
          <Name>Etch A Sketch</Name>
          <CategoryId>1</CategoryId>
        </Product>
      </Products>
    </Category>
    <Category>
      <Id>2</Id>
      <Name>Home &amp; Garden</Name>
      <CatalogId>1</CatalogId>
      <ParentCategoryId xsi:nil=""true"" />
      <Subcategories />
      <Products>
        <Product>
          <Id>3</Id>
          <Name>Adirondack Chair</Name>
          <CategoryId>2</CategoryId>
        </Product>
      </Products>
    </Category>
  </Categories>
</Catalog>");
        }
    }

    public class DeserializeTests : CatalogRepositoryTestBase
    {
        private void WhenTheCatalogIsDeserialized(string xml)
        {
            var serializer = new Serializer();
            serializer.Deserialize(xml);
        }

        [Fact]
        public void Deserialize_Catalog_With_Categories_And_Products()
        {
            var xml =
@"<?xml version=""1.0"" encoding=""utf-16""?>
<Catalog xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Id>1</Id>
  <Name>Department Store</Name>
  <Categories>
    <Category>
      <Id>1</Id>
      <Name>Toys</Name>
      <CatalogId>1</CatalogId>
      <ParentCategoryId xsi:nil=""true"" />
      <Subcategories />
      <Products>
        <Product>
          <Id>1</Id>
          <Name>Rubik's Cube</Name>
          <CategoryId>1</CategoryId>
        </Product>
        <Product>
          <Id>2</Id>
          <Name>Etch A Sketch</Name>
          <CategoryId>1</CategoryId>
        </Product>
      </Products>
    </Category>
    <Category>
      <Id>2</Id>
      <Name>Home &amp; Garden</Name>
      <CatalogId>1</CatalogId>
      <ParentCategoryId xsi:nil=""true"" />
      <Subcategories />
      <Products>
        <Product>
          <Id>3</Id>
          <Name>Adirondack Chair</Name>
          <CategoryId>2</CategoryId>
        </Product>
      </Products>
    </Category>
  </Categories>
</Catalog>";
            WhenTheCatalogIsDeserialized(xml);
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
    }
}
