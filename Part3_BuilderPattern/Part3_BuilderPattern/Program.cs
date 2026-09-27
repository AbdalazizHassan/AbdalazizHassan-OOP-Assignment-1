namespace Part3_BuilderPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Task 3: Builder Pattern with Mandatory Constructors ===\n");

            Address billingAddress = new AddressBuilder("123 Main Street", "Cairo")
                .WithCountry("Egypt")                                               
                .WithZipCode("11511")                                               
                .Build();

            Address shippingAddress = new AddressBuilder("456 Branch Street", "Giza") 
                .WithCountry("Egypt")                                                  
                .Build();

           
            Invoice invoice = new InvoiceBuilder(1001, "Abdalaziz Hassan") 
                .WithContactInfo("abdalaziz@example.com", "01012345678")  
                .WithBillingAddress(billingAddress)                        
                .WithShippingAddress(shippingAddress)                      
                .WithOrderDetails(subTotal: 500m, taxAmount: 70m, discountAmount: 20m, currency: "EGP")
                .Build();

            Console.WriteLine($"Invoice #{invoice.InvoiceId} created for {invoice.CustomerName}");
            Console.WriteLine($"Billing Address: {invoice.BillingAddress.Street}, {invoice.BillingAddress.City}");
            Console.WriteLine($"Total Amount: {invoice.TotalAmount} {invoice.Currency}");
        }    
    }
}
