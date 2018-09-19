using System.Linq;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public class OrderRepository
    {
        private Context _context;

        public OrderRepository(Context context)
        {
            _context = context;
        }

        public void Create(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
        }

        public Order GetByCustomerName(string customerName)
        {
            return _context.Orders.SingleOrDefault(o => o.Customer.Name == customerName);
        }
    }
}
