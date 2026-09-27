using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_BuilderPattern
{
    public class AddressBuilder
    {
        private readonly string _street;
        private readonly string _city;

        private string _state = string.Empty;
        private string _zipCode = string.Empty;
        private string _country = string.Empty;

        public AddressBuilder(string street, string city)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street is mandatory and cannot be empty.", nameof(street));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is mandatory and cannot be empty.", nameof(city));

            _street = street;
            _city = city;
        }

        public AddressBuilder WithState(string state)
        {
            _state = state ?? string.Empty;
            return this;
        }

        public AddressBuilder WithZipCode(string zipCode)
        {
            _zipCode = zipCode ?? string.Empty;
            return this;
        }

        public AddressBuilder WithCountry(string country)
        {
            _country = country ?? string.Empty;
            return this;
        }

        public Address Build()
        {
            return new Address(_street, _city, _state, _zipCode, _country);
        }
    }
}
