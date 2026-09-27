using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_BuilderPattern
{
    public class InvoiceBuilder
    {
        private readonly int _invoiceId;
        private readonly string _customerName;

        private string _customerEmail = string.Empty;
        private string _customerPhone = string.Empty;

        private Address? _billingAddress;
        private Address? _shippingAddress;

        private DateTime _orderDate = DateTime.UtcNow;
        private string _paymentMethod = "Credit Card";
        private string _currency = "USD";
        private decimal _subTotal;
        private decimal _discountAmount;
        private decimal _taxAmount;

        public InvoiceBuilder(int invoiceId, string customerName)
        {
            if (invoiceId <= 0)
                throw new ArgumentException("Invoice ID must be positive.", nameof(invoiceId));

            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer Name is mandatory.", nameof(customerName));

            _invoiceId = invoiceId;
            _customerName = customerName;
        }

        public InvoiceBuilder WithContactInfo(string email, string phone)
        {
            _customerEmail = email ?? string.Empty;
            _customerPhone = phone ?? string.Empty;
            return this;
        }

        public InvoiceBuilder WithBillingAddress(Address address)
        {
            _billingAddress = address ?? throw new ArgumentNullException(nameof(address), "Billing address cannot be null.");
            return this;
        }

        public InvoiceBuilder WithShippingAddress(Address address)
        {
            _shippingAddress = address;
            return this;
        }

        public InvoiceBuilder WithOrderDetails(decimal subTotal, decimal taxAmount, decimal discountAmount = 0, string currency = "USD")
        {
            if (subTotal < 0)
                throw new ArgumentException("SubTotal cannot be negative.");

            _subTotal = subTotal;
            _taxAmount = taxAmount;
            _discountAmount = discountAmount;
            _currency = currency;
            return this;
        }

        public InvoiceBuilder WithPaymentMethod(string paymentMethod)
        {
            _paymentMethod = paymentMethod;
            return this;
        }

        public Invoice Build()
        {
            if (_billingAddress == null)
                throw new InvalidOperationException("Billing Address is mandatory before building the Invoice.");

            _shippingAddress ??= _billingAddress;

            decimal totalAmount = (_subTotal + _taxAmount) - _discountAmount;

            return new Invoice(
                _invoiceId,
                _customerName,
                _customerEmail,
                _customerPhone,
                _billingAddress,
                _shippingAddress,
                _orderDate,
                _paymentMethod,
                _currency,
                _subTotal,
                _discountAmount,
                _taxAmount,
                totalAmount
            );
        }
    }
}
