using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1_ProceduralToOOP
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string City { get; set; } = null!;
        public bool IsVip { get; set; }

        public Customer(int id, string name, string email, string city, bool isVip)
        {
            Id = id;
            Name = name;
            Email = email;
            City = city;
            IsVip = isVip;
        }
    }
}
