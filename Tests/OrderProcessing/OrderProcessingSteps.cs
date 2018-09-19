using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using DataAccessLayer;
using DataAccessLayer.Models;
using TechTalk.SpecFlow;
using Tests.Framework;
using Xunit;

namespace Tests.OrderProcessing
{
    [Binding]
    public class OrderProcessingSteps
    {
        private Context _context;
        private OrderProcessor _orderProcessor;
        private CustomerRepository _customerRepository;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;
        private DbTestTransaction _dbTestTransaction;

        public OrderProcessingSteps()
        {
            _context = new Context();
            _customerRepository = new CustomerRepository(_context);
            _productRepository = new ProductRepository(_context);
            _orderRepository = new OrderRepository(_context);
            _orderProcessor = new OrderProcessor(_orderRepository, _productRepository, _customerRepository);
        }

        private Order BuildOrder(string customerName, string productName, int qty)
        {
            var customer = _customerRepository.GetByName(customerName);
            var product = _productRepository.GetByName(productName);

            return new Order
            {
                CustomerId = customer.Id,
                ProductId = product.Id,
                Qty = qty
            };
        }

        [Given(@"(.*) has no discount")]
        public void GivenCUSTOMERHasNoDiscount(string customerName)
        {
            GivenCUSTOMERHasAPERCENTDiscount(customerName, 0);
        }

        [Given(@"(.*) has a (.*)% discount")]
        public void GivenCUSTOMERHasAPERCENTDiscount(string customerName, int discountPercent)
        {
            var customer = new Customer
            {
                Name = customerName,
                DiscountPercent = discountPercent
            };

            _customerRepository.Create(customer);
        }

        [Given(@"(.*) costs \$(.*)")]
        public void GivenPRODUCTCostsPRICE(string productName, decimal price)
        {
            var product = new Product
            {
                Name = productName,
                Price = price
            };

            _productRepository.Create(product);
        }

        [When(@"(.*) orders one (.*)")]
        public void WhenCUSTOMEROrdersOnePRODUCT(string customerName, string productName)
        {
            var orders = new List<Order> { BuildOrder(customerName, productName, 1) };

            _orderProcessor.Process(orders);
        }

        [When(@"a batch of orders is processed:")]
        public void WhenABatchOfOrdersIsProcessed(Table table)
        {
            var orders = table.Rows
                .Select(row => BuildOrder(row["Customer"], row["Product"], Convert.ToInt32(row["Qty"])));

            _orderProcessor.Process(orders);
        }

        [Then(@"the total cost for (.*) is \$(.*)")]
        public void ThenTheTotalCostForCUSTOMERIsTOTALCOST(string customerName, decimal totalCost)
        {
            var order = _orderRepository.GetByCustomerName(customerName);
            Assert.Equal(totalCost, order.TotalCost);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _dbTestTransaction = new DbTestTransaction();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _dbTestTransaction.Dispose();
        }
    }

    public class DbTestTransaction : DbTestBase
    {
    }
}
