using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Models;
using Tests.Framework;
using Xunit;

namespace Tests
{
    public abstract class CatalogRepositoryTestBase : DbTestBase
    {
        protected void GivenIdentitySeedsAreReset()
        {
            DbTestUtilities.ResetIdentitySeed("Catalogs");
            DbTestUtilities.ResetIdentitySeed("Categories");
            DbTestUtilities.ResetIdentitySeed("Products");
        }

        protected void WhenCatalogsAreCreated(params Catalog[] entities)
        {
            var repository = new CatalogRepository();

            foreach (var entity in entities)
                repository.Create(entity);
        }

        protected void WhenCatalogIsCreated(Catalog entity)
        {
            WhenCatalogsAreCreated(entity);
        }

        protected List<Catalog> ThenThereShouldBeResults(int expectedCount)
        {
            var repository = new CatalogRepository();
            var entities = repository.GetAll();
            Assert.Equal(expectedCount, entities.Count);
            return entities;
        }

        protected Catalog ThenThereShouldBeOneResult()
        {
            var entities = ThenThereShouldBeResults(1);
            return entities[0];
        }
    }
}
