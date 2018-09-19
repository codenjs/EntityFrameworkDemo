using System.Linq;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class ProductRepository
    {
        private Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public void Create(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public Product GetById(int id)
        {
            return _context.Products.SingleOrDefault(p => p.Id == id);
        }

        public Product GetByName(string name)
        {
            return _context.Products.SingleOrDefault(p => p.Name == name);
        }
    }
}
