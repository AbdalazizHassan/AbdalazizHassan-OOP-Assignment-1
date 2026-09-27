using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1_ProceduralToOOP
{
    public class OrderLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public double GetLineTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
