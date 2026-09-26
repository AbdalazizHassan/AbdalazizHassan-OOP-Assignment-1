using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1_ProceduralToOOP
{
    public class OrderSystemManager
    {
        private readonly List<Customer> _customers = new();
        private readonly List<Product> _products = new();
        private readonly List<Order> _orders = new();

        public void AddCustomer(int id, string name, string email, string city, bool isVip)
        {
            if (_customers.Any(c => c.Id == id))
            {
                Console.WriteLine($"ERROR: customer id {id} already exists.");
                return;
            }
            _customers.Add(new Customer(id, name, email, city, isVip));
        }

        public void PrintCustomers()
        {
            Console.WriteLine($"\n=== CUSTOMERS ({_customers.Count}) ===");
            foreach (var c in _customers)
            {
                string vipStatus = c.IsVip ? "yes" : "no";
                Console.WriteLine($"#{c.Id}  {c.Name}  <{c.Email}>  {c.City}  vip={vipStatus}");
            }
        }

        public void AddProduct(int id, string name, double price, int stock)
        {
            if (_products.Any(p => p.Id == id))
            {
                Console.WriteLine($"ERROR: product id {id} already exists.");
                return;
            }
            _products.Add(new Product(id, name, price, stock));
        }

        public void PrintProducts()
        {
            Console.WriteLine($"\n=== PRODUCTS ({_products.Count}) ===");
            foreach (var p in _products)
            {
                Console.WriteLine($"#{p.Id}  {p.Name}  price={p.Price:F2}  stock={p.Stock}");
            }
        }

        public void CreateOrder(int orderId, int customerId, string date)
        {
            if (_orders.Any(o => o.Id == orderId))
            {
                Console.WriteLine($"ERROR: order id {orderId} already exists.");
                return;
            }

            var customer = _customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine($"ERROR: customer id {customerId} not found.");
                return;
            }

            _orders.Add(new Order(orderId, customer, date));
        }

        public void AddLineToOrder(int orderId, int productId, int quantity)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                Console.WriteLine($"ERROR: order id {orderId} not found.");
                return;
            }

            if (order.IsPaid)
            {
                Console.WriteLine("ERROR: cannot change a paid order.");
                return;
            }

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine($"ERROR: product id {productId} not found.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("ERROR: quantity must be positive.");
                return;
            }

            if (product.Stock < quantity)
            {
                Console.WriteLine($"ERROR: not enough stock for product #{productId}.");
                return;
            }

            product.Stock -= quantity;
            order.Lines.Add(new OrderLine(product, quantity));
        }

        public void MarkOrderPaid(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                Console.WriteLine($"ERROR: order id {orderId} not found.");
                return;
            }

            if (order.Lines.Count == 0)
            {
                Console.WriteLine("ERROR: cannot pay an empty order.");
                return;
            }

            order.IsPaid = true;
        }

        public void PrintOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                Console.WriteLine($"ERROR: order id {orderId} not found.");
                return;
            }

            string paidStatus = order.IsPaid ? "yes" : "no";
            Console.WriteLine($"\n=== ORDER #{order.Id} ===");
            Console.WriteLine($"Date: {order.Date}");
            Console.WriteLine($"Customer: {order.Customer.Name} (#{order.Customer.Id})");
            Console.WriteLine($"Paid: {paidStatus}");
            Console.WriteLine("Lines:");

            foreach (var line in order.Lines)
            {
                Console.WriteLine($"  - {line.Product.Name}  x{line.Quantity}  @{line.Product.Price:F2}  = {line.GetLineTotal():F2}");
            }

            Console.WriteLine($"TOTAL: {order.CalculateTotal():F2}");
        }

        public void PrintAllOrders()
        {
            Console.WriteLine($"\n=== ALL ORDERS ({_orders.Count}) ===");
            foreach (var order in _orders)
            {
                PrintOrder(order.Id);
            }
        }

        public double TotalSalesPaidOnly()
        {
            return _orders.Where(o => o.IsPaid).Sum(o => o.CalculateTotal());
        }

        public void SeedSampleData()
        {
            AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
            AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
            AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

            AddProduct(101, "USB Cable", 50.0, 100);
            AddProduct(102, "Wireless Mouse", 250.0, 40);
            AddProduct(103, "Mechanical Keyboard", 1200.0, 15);
            AddProduct(104, "Laptop Stand", 400.0, 25);
        }

        public void RunDemoScenario()
        {
            CreateOrder(1001, 1, "2026-09-15");
            AddLineToOrder(1001, 101, 2);
            AddLineToOrder(1001, 102, 1);
            MarkOrderPaid(1001);

            CreateOrder(1002, 2, "2026-09-15");
            AddLineToOrder(1002, 103, 1);
            AddLineToOrder(1002, 104, 1);

            CreateOrder(1003, 3, "2026-09-16");
            AddLineToOrder(1003, 101, 5);
            MarkOrderPaid(1003);
        }
    }
}
