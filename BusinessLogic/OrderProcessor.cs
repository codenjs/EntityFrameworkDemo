using System.Collections.Generic;
using DataAccessLayer.Models;
using DataAccessLayer;

namespace BusinessLogic
{
    public class OrderProcessor
    {
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        private CustomerRepository _customerRepository;

        public OrderProcessor(OrderRepository orderRepository,
            ProductRepository productRepository,
            CustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public void Process(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                var product = _productRepository.GetById(order.ProductId);
                var customer = _customerRepository.GetById(order.CustomerId);

                if (customer.DiscountPercent > 0)
                    product.Price = product.Price * customer.DiscountPercent / 100;

                order.TotalCost = order.Qty * product.Price;

                _orderRepository.Create(order);
            }
        }
    }
}
