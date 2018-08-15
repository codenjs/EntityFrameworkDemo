using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class CatalogRepositoryWithInjectedContext
    {
        private Context _context;

        public CatalogRepositoryWithInjectedContext(Context context)
        {
            _context = context;
        }

        public void Create(Catalog entity)
        {
            _context.Catalogs.Add(entity);
            _context.SaveChanges();
        }

        public List<Catalog> GetAll()
        {
            return _context.Catalogs
                .Include("Categories.Products")
                .Include("Categories.Subcategories")
                .ToList();
        }
    }
}
