using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class CatalogRepository
    {
        public void Create(Catalog entity)
        {
            using (var context = new Context())
            {
                context.Catalogs.Add(entity);
                context.SaveChanges();
            }
        }

        public List<Catalog> GetAll()
        {
            using (var context = new Context())
            {
                return context.Catalogs
                    .Include("Categories.Products")
                    .Include("Categories.Subcategories")
                    .ToList();
            }
        }
    }
}
