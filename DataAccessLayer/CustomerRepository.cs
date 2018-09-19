using System.Linq;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class CustomerRepository
    {
        private Context _context;

        public CustomerRepository(Context context)
        {
            _context = context;
        }

        public void Create(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.SaveChanges();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.SingleOrDefault(p => p.Id == id);
        }

        public Customer GetByName(string name)
        {
            return _context.Customers.SingleOrDefault(c => c.Name == name);
        }
    }
}
