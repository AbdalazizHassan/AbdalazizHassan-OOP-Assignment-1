using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1_ProceduralToOOP
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string Date { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();

        public Order(int id, Customer customer, string date)
        {
            Id = id;
            Customer = customer;
            Date = date;
            IsPaid = false;
        }

        public double CalculateTotal()
        {
            double total = 0.0;
            foreach (var line in Lines)
            {
                total += line.GetLineTotal();
            }

            if (Customer.IsVip)
            {
                total *= 0.90;
            }

            return total;
        }
    }
}
